namespace ChatClient;

public interface IClientServer
{
    void ConnectToServer();

    bool ClientConnect();

    Task<string> TrySendMessageAsync(string message);
}