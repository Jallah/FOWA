using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using FowaProtocol.FowaMessages;
using FowaProtocol.FowaModels;
using FowaProtocol.XmlDeserialization;
using Server;
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
            var s = Singleton.Instance; 

            var a = Singleton.Instance;

            Console.ReadKey(); 
        }
    }
}
