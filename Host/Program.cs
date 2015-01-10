using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverAddress = "http://localhost:9000";
            WebApp.Start<Signa.Startup>(serverAddress);

            Console.ReadLine();
        }
    }
}
