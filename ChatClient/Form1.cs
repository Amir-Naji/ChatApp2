using Helpers;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ChatClient;

public partial class Form1 : Form
{
    delegate void SafeCallDelegate(string text);
    private readonly IClientServer _cs = new ClientServer(new TcpClient());
    IConverters _converters = new Converters();
    TcpClient client = new TcpClient();
    NetworkStream ns = default(NetworkStream);
    string readData = null;
    private Thread thread;

    public Form1()
    {
        InitializeComponent();
    }

    private void btnSendMessage_Click(object sender, EventArgs e)
    {
        NewSendMessage();
    }

    private void txtSendMessage_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) 
            NewSendMessage();
    }

    private void NewSendMessage()
    {
        byte[] buffer = Encoding.ASCII.GetBytes(txtSendMessage.Text);
        ns.Write(buffer, 0, buffer.Length);

        txtSendMessage.Text = string.Empty;
    }

    //private async Task SendMessage()
    //{
    //    _cs.ConnectToServer();
    //    var s = await _cs.TrySendMessageAsync(txtSendMessage.Text);
    //    //var s = sendMessage(txtSendMessage.Text);
    //    txtSendMessage.Text = string.Empty;
    //    txtAllMessages.Text = txtAllMessages.Text + Environment.NewLine + s;
    //}

    private void Form1_Load(object sender, EventArgs e)
    {
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

    private void ReceiveData(TcpClient client, TextBox tb)
    {
        NetworkStream ns = client.GetStream();
        byte[] receivedBytes = new byte[1024];
        int byte_count;
        var returnString = string.Empty;

        while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
        {
            var v = Encoding.ASCII.GetString(receivedBytes, 0, byte_count);
            SetText(v);
            Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
        }
    }

    private void SetText(string text)
    {
        if (this.txtAllMessages.InvokeRequired)
        {
            SafeCallDelegate d = new SafeCallDelegate(SetText);
            this.Invoke(d, new object[] { text });
        }
        else
        {
            this.txtAllMessages.Text += text;
        }
    }
}