// See https://aka.ms/new-console-template for more information

using ChatServer;
using Helpers;

var rs = new RemoteServer(new ChatLog());
await rs.Start();