using System.Net.Sockets;
using ChatServer;

namespace ChatServerTests;

[TestFixture]
public class RemoteServerTests
{
    [TearDown]
    public void TearDown()
    {
        server.Stop();
    }

    private RemoteServer server;


    [Test]
    public async Task Start_NoInput_False()
    {
        server = new RemoteServer();
        server.Start();

        Assert.IsFalse(server.ServerConnect());
    }

    //[Test]
    //public async Task ClientConnect_NoInput_True()
    //{
    //    await Init();

    //    Assert.IsTrue(server.ClientConnect());
    //}

    [Test]
    public async Task ReceiveMessage_NullInput_NullReferenceException()
    {
        await Init();

        Assert.CatchAsync<NullReferenceException>(() => server.ReceiveMessage(null));
    }

    [Test]
    public async Task ReceiveMessage_NewObject_InvalidOperationException()
    {
        await Init();

        Assert.CatchAsync<InvalidOperationException>(() => server.ReceiveMessage(new TcpClient()));
    }

    [Test]
    public async Task Stop_NoInput_InvalidOperationException()
    {
        await Init();
        server.Stop();

        Assert.Catch<InvalidOperationException>(() => server.ServerConnect());
    }

    private async Task Init()
    {
        server = new RemoteServer();
        server.Start();
    }
}