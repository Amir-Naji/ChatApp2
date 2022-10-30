using System.Net;
using System.Net.Sockets;
using System.Text;
using Helpers;

namespace ChatClient;

public class ClientServer : IClientServer
{
    private readonly IConverters _converters = new Converters();
    private TcpClient _client;
    private Thread thread;
    private delegate void SafeCallDelegate(string text);

    private Action<string>  _safeCallDelegate;

    public ClientServer(TcpClient client)
    {
        _client = client;
    }

    public void ConnectToServer()
    {
        var ip = IPAddress.Parse("127.0.0.1");
        var port = 5000;
        _client = new TcpClient();
        if (!ClientConnect())
            _client.Connect(ip, port);

        CreateThread();

        // Need to change it to log file
        Console.WriteLine("client connected!!");

        //ns = _client.GetStream();
    }

    public void SendMessage(string message)
    {
        var ns = _client.GetStream();
        var buffer = Encoding.ASCII.GetBytes(message);
        ns.Write(buffer, 0, buffer.Length);
    }

    public bool ClientConnect()
    {
        return _client.Connected;
    }

    public void SetText(Action<string> safeCallDelegate)
    {
        _safeCallDelegate = safeCallDelegate;
    }

    private void CreateThread()
    {
        thread = new Thread(o => { ReceiveData(o as TcpClient); });

        thread.Start(_client);
    }

    private void ReceiveData(TcpClient client)
    {
        var ns = client.GetStream();
        var receivedBytes = new byte[1024];
        int byteCount;

        while ((byteCount = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
        {
            var v = Encoding.ASCII.GetString(receivedBytes, 0, byteCount);
            _safeCallDelegate(v);

            // Need to log in file
            Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byteCount));
        }
    }
}