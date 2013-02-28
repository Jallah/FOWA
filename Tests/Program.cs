using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.BL.Services;


namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {

            UserFriendsService service = new UserFriendsService();

            service.AddUser(new user{email = "hans@gmx.net", nick = "hans", pw = "superSavePw"});

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
