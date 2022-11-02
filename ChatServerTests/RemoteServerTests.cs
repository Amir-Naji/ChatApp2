using System.Net.Sockets;
using ChatServer;
using Helpers;

namespace ChatServerTests;

[TestFixture]
public class RemoteServerTests
{
    [TearDown]
    public void TearDown()
    {
        _server.Stop();
    }

    private RemoteServer _server;


    [Test]
    public void Start_NoInput_False()
    {
        _server = new RemoteServer(new ChatLog());
        _server.Start();

        Assert.IsFalse(_server.ServerConnect());
    }

    [Test]
    public void ServerConnect_NoStartServer_Exception()
    {
        _server = new RemoteServer(new ChatLog());
        Assert.Throws<InvalidOperationException>(() => _server.ServerConnect());
    }

    //[Test]
    //public async Task ClientConnect_NoInput_True()
    //{
    //    await Init();

    //    Assert.IsTrue(server.ClientConnect());
    //}

    [Test]
    public void Stop_NoInput_InvalidOperationException()
    {
        Init();
        _server.Stop();

        Assert.Catch<InvalidOperationException>(() => _server.ServerConnect());
    }

    private async Task Init()
    {
        _server = new RemoteServer(new ChatLog());
        await _server.Start();
    }
}