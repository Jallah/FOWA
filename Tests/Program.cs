using System;
using FowaProtocol.FowaMessages;
using FowaProtocol.MessageEnums;
using FowaProtocol.XmlDeserialization;
using Server.BL.Services;
using Server.DL;

namespace Tests
{
    public sealed class Singleton
    {
        private static readonly Singleton instance = new Singleton();

        private Singleton() { Console.WriteLine("Ctor"); }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            //var s1 = Singleton.Instance; 
            //var s2 = Singleton.Instance;
            //var s3 = Singleton.Instance;

            UserFriendsService service = new UserFriendsService();
            //var user = service.GetUserById(1);
            //var user2 = service.GetUserById(2);

            //var friends = service.GetFriends(user);

            //foreach (var friend in friends)
            //{
            //    Console.WriteLine(friend.email);
            //}
            ErrorMessage m = new ErrorMessage(ErrorMessageKind.LiginError, "hans");

            

            Console.WriteLine(XmlDeserializer.GetErrorMessage(m.Message));
            Console.ReadKey(); 
        }
    }
}
