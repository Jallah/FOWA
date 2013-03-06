using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.BL.Services;
using Server.DL;


namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpListener listener = new TcpListener(IPAddress.Any /*IPAddress.Parse("127.0.0.1")*/, 80);
            listener.Start();

            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();

            StreamReader reader = new StreamReader(stream);

            while (true)
            {
                string s = reader.ReadLine();
                Console.WriteLine(s);
            }
            //UserFriendsService service = new UserFriendsService();

            //bool exist = service.UserExists("hhans@gmx.net");

            //string s = exist ? "yes" : "no";

            //Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
