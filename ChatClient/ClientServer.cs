using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Helpers;

namespace ChatClient;

public class ClientServer : IClientServer
{
    private readonly IConverters _converters = new Converters();
    private TcpClient _client;

    public ClientServer(TcpClient client)
    {
        _client = client;
    }

    public void ConnectToServer()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 5000;
        client = new TcpClient();
        if (!client.Connected)
            client.Connect(ip, port);
        Console.WriteLine("client connected!!");
        ns = client.GetStream();
        thread = new Thread(o =>
        {
            ReceiveData((TcpClient)o, txtAllMessages);
        });

        thread.Start(client);
    }

    public bool ClientConnect()
    {
        return _client.Connected;
    }

    public async Task<string> TrySendMessageAsync(string message)
    {
        try
        {
            return await SendMessage(message);
            //return sendMessage(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return string.Empty;
        }
        //finally
        //{
        //    _client.Close();
        //}
    }

    private async Task<string> SendMessage(string message)
    {
        
        await using var stream = _client.GetStream();
        SendToServer(message, stream);
        var returnValue = await _converters.StreamToString(stream);

        return returnValue;
    }

    private void SendToServer(string message, Stream stream)
    {
        var messageBytes = _converters.StringMessageToByteArray(message);
        stream.Write(messageBytes, 0, messageBytes.Length);
    }

    //public string sendMessage(string message)
    //{
    //    string response = "";
    //    try
    //    {
    //        TcpClient client = new TcpClient("127.0.0.1", 1337); // Create a new connection  
    //        client.NoDelay = true; // please check TcpClient for more optimization
    //        // messageToByteArray- discussed later
    //        byte[] messageBytes = _converters.StringMessageToByteArray(message);

    //        using (NetworkStream stream = client.GetStream())
    //        {
    //            stream.Write(messageBytes, 0, messageBytes.Length);

    //            // Message sent!  Wait for the response stream of bytes...
    //            // streamToMessage - discussed later
    //            response = _converters.StreamToString(stream).Result;
    //        }
    //        client.Close();
    //    }
    //    catch (Exception e) { Console.WriteLine(e.Message); }
    //    return response;
    //}
}