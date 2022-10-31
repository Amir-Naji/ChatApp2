using System.Net.Sockets;

namespace ChatServer.Models;

public class Client
{
    public TcpClient tcpClient { get; set; }

    public string Username { get; set; }
}