using Microsoft.Owin.Hosting;
using Signa;
using Signa.Data;
using Signa.Recognizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
