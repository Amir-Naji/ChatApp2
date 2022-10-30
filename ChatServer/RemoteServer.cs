using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Helpers;

namespace ChatServer;

public class RemoteServer
{
    private readonly IConverters _converters;
    private readonly TcpListener _listener;
    private Hashtable _clientsList = new Hashtable();
    static readonly object _lock = new object();
    static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();

    public RemoteServer()
    {
        //_listener = new TcpListener(IPAddress.Any, 1337);
        //Console.WriteLine("The server started");
        //_listener.Start();
        //_converters = new Converters();

        int count = 1;

        TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
        ServerSocket.Start();

        while (true)
        {
            TcpClient client = ServerSocket.AcceptTcpClient();
            lock (_lock) list_clients.Add(count, client);
            Console.WriteLine("Someone connected!!");

            Thread t = new Thread(handle_clients);
            t.Start(count);
            count++;
        }
    }

    public async Task Start()
    {
        await AcceptClient(_listener);
    }

    public static void handle_clients(object o)
    {
        int id = (int)o;
        TcpClient client;

        lock (_lock) client = list_clients[id];

        while (true)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byte_count = stream.Read(buffer, 0, buffer.Length);

            if (byte_count == 0)
            {
                break;
            }

            string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
            broadcast(data);
            Console.WriteLine(data);
        }

        lock (_lock) list_clients.Remove(id);
        client.Client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    public static void broadcast(string data)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

        lock (_lock)
        {
            foreach (TcpClient c in list_clients.Values)
            {
                NetworkStream stream = c.GetStream();

                stream.Write(buffer, 0, buffer.Length);
            }
        }
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