using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace releaser
{
    class Program
    {
        private static int port;
        private static TcpListener server;
        private static TcpClient client = new TcpClient();
        private static string msg;
        private static StreamWriter msgWriter;

        static void Main(string[] args)
        {
            Console.WriteLine("Message Sender");
            Console.WriteLine("--------------");
            Console.WriteLine("");

            port = Convert.ToInt32(File.ReadAllText(@"settings.ini")); //Get the port
            msg = File.ReadAllText(@"msg.txt"); //Get the msg

            Console.WriteLine("Port:- " + port);
            Console.WriteLine("Press any key to start the server...");
            Console.Read();
            server = new TcpListener(port);

            try
            {
                server.Start();
                Console.WriteLine("Server started");

                #region Server function

                while (true)
                {
                    client = server.AcceptTcpClient();
                    Console.WriteLine("");
                    Console.WriteLine("Someone Connected from - " + client.Client.LocalEndPoint);
                    msgWriter = new StreamWriter(client.GetStream());
                    msgWriter.WriteLine(msg);
                    msgWriter.Close();
                    Console.WriteLine("Message sent successfully");
                }

                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("Server ran onto an error");
                Console.Write(ex.Message);
                Console.Read();
                
            }
        }
    }
}
