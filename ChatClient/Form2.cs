using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChatClient
{
    public partial class Form2 : Form
    {
        TcpClient client = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        string readData = null;
        private Thread thread;

        public Form2()
        {
            InitializeComponent();
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            client = new TcpClient();
            if (!client.Connected)
                client.Connect(ip, port);
            Console.WriteLine("client connected!!");
            ns = client.GetStream();
            thread = new Thread(o => ReceiveData((TcpClient)o));

            thread.Start(client);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(textBox1.Text);
            ns.Write(buffer, 0, buffer.Length);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            ns.Close();
            client.Close();
            Console.WriteLine("disconnect from server!!");
            Console.ReadKey();
        }

        private static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
            }
        }
    }
}
