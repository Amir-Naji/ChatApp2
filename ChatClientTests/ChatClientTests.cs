using System.Net.Sockets;
using ChatClient;
using ChatServer;

namespace ChatClientTests;

public class ChatClientTests
{
    private IClientServer client;
    private RemoteServer server;

    [Test]
    public async Task ClientConnect_NoInput_True()
    {
        await Init();
        client = new ClientServer(await server.Listener().AcceptTcpClientAsync());
        client.ConnectToServer();

        Assert.IsTrue(client.ClientConnect());
    }

    //[Test]
    //public async Task SendMessage_EmptyString_ReturnTrue()
    //{
    //    await Init();
    //    client = new ClientServer(await server.Listener().AcceptTcpClientAsync());
    //    var v = await client.TrySendMessageAsync(string.Empty);
    //    Assert.IsTrue(v);
    //}

    //[Test]
    //public async Task SendMessage_NoServer_ReturnFalse()
    //{
    //    client = new ClientServer(await server.Listener().AcceptTcpClientAsync());
    //    var v = await client.TrySendMessageAsync(string.Empty);
    //    Assert.IsFalse(v);
    //}
    //[Test]
    //public async Task ReceiveMessage_NullInput_NullReferenceException()
    //{
    //    await Init();

    //    Assert.CatchAsync<NullReferenceException>(() => server.ReceiveMessage(null));
    //}

    //[Test]
    //public async Task ReceiveMessage_NewObject_InvalidOperationException()
    //{
    //    await Init();

    //    Assert.CatchAsync<InvalidOperationException>(() => server.ReceiveMessage(new TcpClient()));
    //}

    private async Task Init()
    {
        server = new RemoteServer();
        server.Start();
    }
}