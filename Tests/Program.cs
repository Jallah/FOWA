using System;

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
            var s1 = Singleton.Instance; 
            var s2 = Singleton.Instance;
            var s3 = Singleton.Instance;

            Console.ReadKey(); 
        }
    }
}
