// See https://aka.ms/new-console-template for more information

using ChatServer;
using Helpers;
using System.Net.Sockets;
using System.Net;
using System;

IConverters _converters = new Converters();
//Console.WriteLine("Hello, World!");
//RemoteServer rs = new RemoteServer();
//while (true)
//{
//    rs.Start();
//}


//TestServer.Run();

var rs = new RemoteServer();
await rs.Start();
//Console.WriteLine("Server starting !");

// IP Address to listen on. Loopback in this case
//IPAddress ipAddr = IPAddress.Loopback;
//// Port to listen on
//int port = 1337;
//// Create a network endpoint
//IPEndPoint ep = new IPEndPoint(ipAddr, port);
//// Create and start a TCP listener
//TcpListener listener = new TcpListener(ep);
//listener.Start();

//Console.WriteLine("Server listening on: {0}:{1}", ep.Address, ep.Port);

//// keep running
//while (true)
//{
//    var sender = listener.AcceptTcpClient();
//    // streamToMessage - discussed later
//    string request = await _converters.StreamToString(sender.GetStream());
//    if (request != null)
//    {
//        string responseMessage = MessageHandler(request);
//        sendMessage(responseMessage, sender);
//    }
//}

//void sendMessage(string message, TcpClient client)
//{
//    // messageToByteArray- discussed later
//    byte[] bytes = _converters.StringMessageToByteArray(message);
//    client.GetStream().Write(bytes, 0, bytes.Length);
//}

//static string MessageHandler(string message)
//{
//    Console.WriteLine("Received message: " + message);
//    return message;
//}