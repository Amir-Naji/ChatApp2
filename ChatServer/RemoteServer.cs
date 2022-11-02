using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatServer.Models;
using Helpers;

namespace ChatServer;

public class RemoteServer
{
    private readonly Dictionary<int, Client> _listClients = new();
    private readonly TcpListener _listener;
    private readonly object _lock = new object();
    private readonly IChatLog _chatLog;
    private int _count;

    public RemoteServer(IChatLog chatLog)
    {
        _chatLog = chatLog;
        _listener = new TcpListener(IPAddress.Any, 5000);
    }

    public async Task Start()
    {
        _listener.Start();
        await AcceptClient(_listener);
    }

    private async Task AcceptClient(TcpListener listener)
    {
        var cancelToken = new CancellationTokenSource();
        _count = 1;

        while (!cancelToken.IsCancellationRequested)
        {
            var tcpClient = await listener.AcceptTcpClientAsync();
            HandleTcpClient(tcpClient);

            _count++;
        }
    }

    private void HandleTcpClient(TcpClient sender)
    {
        lock (_lock)
        {
            _listClients.Add(
                _count,
                new Client
                {
                    tcpClient = sender
                });
        }

        _chatLog.LogInfo("Someone connected!!");

        var t = new Thread(HandleClients);
        t.Start(_count);
    }

    private void HandleClients(object o)
    {
        var id = (int)o;
        TcpClient client;

        lock (_lock)
        {
            client = _listClients[id].tcpClient;
        }

        ClientMessage(client);

        lock (_lock)
        {
            _listClients.Remove(id);
        }

        client.Client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    private void ClientMessage(TcpClient client)
    {
        while (true)
        {
            var stream = client.GetStream();
            var buffer = new byte[1024];
            var byteCount = stream.Read(buffer, 0, buffer.Length);

            if (byteCount == 0) break;

            var data = Encoding.ASCII.GetString(buffer, 0, byteCount);

            Broadcast(CheckMessage(data));
            _chatLog.LogInfo(data);
        }
    }

    private string CheckMessage(string message)
    {
        if (!message.StartsWith("Username>")) 
            return message;

        message = message[9..];
        lock (_lock)
        {
            _listClients[_count - 1].Username = message;
        }

        return $"{message} has joined the chat.";
    }

    private void Broadcast(string data)
    {
        var buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

        lock (_lock)
        {
            foreach (var stream in _listClients.Values.Select(c => c.tcpClient.GetStream()))
                stream.Write(buffer, 0, buffer.Length);
        }
    }

    public bool ServerConnect()
    {
        return _listener.Pending();
    }

    public void Stop()
    {
        _listener.Stop();
    }

    public TcpListener Listener()
    {
        return _listener;
    }
}