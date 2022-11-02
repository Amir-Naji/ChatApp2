using System.Net.Sockets;

namespace ChatServer.Models;

public class Client
{
    public TcpClient TcpClient { get; set; }

    public string Username { get; set; }
}