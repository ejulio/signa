using Microsoft.Owin.Hosting;
using System;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();
            Console.ReadKey();
        }

        private static void StartServer()
        {
            var serverAddress = "http://localhost:9000";
            WebApp.Start<Signa.Startup>(serverAddress);
            Console.WriteLine("Aplicação iniciada em {0}", serverAddress);
        }
    }
}
