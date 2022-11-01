// See https://aka.ms/new-console-template for more information

using ChatServer;
using Helpers;

IConverters _converters = new Converters();

var rs = new RemoteServer();
await rs.Start();