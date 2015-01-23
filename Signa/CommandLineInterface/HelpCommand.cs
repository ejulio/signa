using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.CommandLineInterface
{
    public class HelpCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Comandos disponíveis");
            Console.WriteLine("carregar-exemplos");
        }

        public bool MatchName(string name)
        {
            return true;
        }
    }
}