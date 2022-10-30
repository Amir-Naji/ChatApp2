using System.Net.Sockets;

namespace ChatClient;

public partial class Form1 : Form
{
    private readonly IClientServer _cs = new ClientServer(new TcpClient());
    private delegate void SafeCallDelegate(string text);

    public Form1()
    {
        InitializeComponent();
        txtSendMessage.Focus();
        txtSendMessage.Select();
        _cs.ConnectToServer();
    }

    private void btnSendMessage_Click(object sender, EventArgs e)
    {
        SendMessage();
    }

    private void txtSendMessage_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            SendMessage();
    }

    private void SendMessage()
    {
        _cs.SetText(SetText);
        _cs.SendMessage(txtSendMessage.Text);
        txtSendMessage.Text = string.Empty;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void SetText(string text)
    {
        if (txtAllMessages.InvokeRequired)
        {
            SafeCallDelegate d = SetText;
            Invoke(d, text);
        }
        else
        {
            txtAllMessages.Text += text;
        }
    }
}