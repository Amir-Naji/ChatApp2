using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient;

public class ClientServer : IClientServer
{
    private TcpClient _client;
    private Thread _thread;

    private Action<string> _safeCallDelegate;

    public ClientServer(TcpClient client)
    {
        _client = client;
    }

    public void ConnectToServer(string username)
    {
        var ip = IPAddress.Parse("127.0.0.1");
        var port = 5000;
        _client = new TcpClient();
        if (!ClientConnect())
            _client.Connect(ip, port);

        CreateThread();

        // Need to change it to log file
        Console.WriteLine("client connected!!");
        SendMessage($"Username>{username}");
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
        _thread = new Thread(o => { ReceiveData(o as TcpClient); });
        _thread.Start(_client);
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