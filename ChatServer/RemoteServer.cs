using System.Collections;
using System.Net;
using System.Net.Sockets;
using Helpers;

namespace ChatServer;

public class RemoteServer
{
    private readonly IConverters _converters;
    private readonly TcpListener _listener;
    private Hashtable _clientsList = new Hashtable();


    public RemoteServer()
    {
        _listener = new TcpListener(IPAddress.Any, 1337);
        Console.WriteLine("The server started");
        _listener.Start();
        _converters = new Converters();
    }

    public async Task Start()
    {
        await AcceptClient(_listener);
    }

    private async Task AcceptClient(TcpListener listener)
    {
        var cancelToken = new CancellationTokenSource();

        while (!cancelToken.IsCancellationRequested)
        {
            var tcpClient = await listener.AcceptTcpClientAsync();
            await HandleTcpClient(tcpClient);
        }
    }

    private async Task HandleTcpClient(TcpClient sender)
    {
        var request = await _converters.StreamToString(sender.GetStream());
        _clientsList.Add(request, sender);
        MessageHandler(request, sender);
        //var v = await ReceiveMessage(client);
        //Console.WriteLine(v);
        //byte[] bytesFrom = new byte[10025];


        //using NetworkStream ns = client.GetStream();

        //var readAsync = await ns.ReadAsync(bytesFrom, 0, (int)client.ReceiveBufferSize);
        //var dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
        //dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
        //Console.WriteLine(dataFromClient);
        //using StreamWriter sw = new StreamWriter(ns);

        //await sw.WriteLineAsync("");
        //await ns.FlushAsync();
    }

    private string MessageHandler(string message, TcpClient sender)
    {
        //foreach (DictionaryEntry item in _clientsList)
        //{
            //var broadcastSocket = (TcpClient)item.Value!;
            //if (broadcastSocket == null)
            //    return string.Empty;

            //var stream = broadcastSocket.GetStream();
            byte[] bytes = _converters.StringMessageToByteArray(message);
            sender.GetStream().Write(bytes, 0, bytes.Length);
        //}
        Console.WriteLine("Received message: " + message);
        //return "Thank a lot for the message!";
        return message;
    }

    public async Task<string> ReceiveMessage(TcpClient client)
    {
        await using var ns = client.GetStream();
        using var sr = new StreamReader(ns);
        return await sr.ReadToEndAsync();
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