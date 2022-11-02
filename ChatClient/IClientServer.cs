namespace ChatClient;

public interface IClientServer
{
    void TryConnectToServer(string username);

    bool ClientConnect();

    void SendMessage(string message);

    void SetText(Action<string> safeCallDelegate);

    void Disconnect();

    //Task<string> TrySendMessageAsync(string message);
}