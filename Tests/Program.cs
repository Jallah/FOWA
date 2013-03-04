using System;
using System.Collections.Generic;
using System.Linq;
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

            UserFriendsService service = new UserFriendsService();

            //service.AddUser(new user{email = "hans@gmx.net", nick = "hans", pw = "superSavePw"});



            bool exist = service.UserExists("hhans@gmx.net");

            string s = exist ? "yes" : "no";

            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
