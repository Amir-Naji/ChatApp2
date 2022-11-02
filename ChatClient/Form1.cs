using Helpers;

namespace ChatClient;

public partial class Form1 : Form
{
    private readonly IChatLog _chatLog;
    private readonly IClientServer _cs;

    public Form1(IClientServer clientServer, IChatLog chatLog)
    {
        InitializeComponent();
        _cs = clientServer;
        _chatLog = chatLog;
        txtUsername.Focus();
        txtUsername.Select();
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
        if (!CheckConnection())
            return;
        _cs.SetText(SetText);
        _cs.SendMessage(WholeMessage());
        txtSendMessage.Text = string.Empty;
    }

    private bool CheckConnection()
    {
        if (txtUsername.Text.Length != 0)
            return true;

        MessageBox.Show("You need to connect first");
        _chatLog.LogInfo("User tried send message without connecting to server first.");
        return false;
    }

    private string WholeMessage()
    {
        return $"{txtUsername.Text}> {txtSendMessage.Text}";
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

    private void btnConnect_Click(object sender, EventArgs e)
    {
        _cs.SetText(SetText);
        _cs.TryConnectToServer(txtUsername.Text);
        btnConnect.Enabled = false;
        txtUsername.Enabled = false;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        _cs.Disconnect();
    }

    private delegate void SafeCallDelegate(string text);
}