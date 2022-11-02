using System.Net.Sockets;
using Helpers;
using SimpleInjector;

namespace ChatClient
{
    internal static class Program
    {
        private static readonly Container _container;

        static Program()
        {
            _container = new Container();

            _container.Register<IClientServer, ClientServer>();
            _container.Register<IChatLog, ChatLog>();

            _container.Verify();
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.Run(new Form1(new ClientServer(new TcpClient(), new ChatLog())));
        }
    }
}