namespace ChatClient;

public interface IClientServer
{
    void ConnectToServer();

    bool ClientConnect();

    void SendMessage(string message);

    void SetText(Action<string> safeCallDelegate);

    //Task<string> TrySendMessageAsync(string message);
}