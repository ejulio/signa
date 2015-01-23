using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.CommandLineInterface
{
    public class LogCommandDecorator : ICommand
    {
        private ICommand command;

        public LogCommandDecorator(ICommand command)
        {
            this.command = command;
        }

        public void Execute()
        {
            Console.WriteLine("Executando comando...");
            command.Execute();
            Console.WriteLine("Comando executado.");
        }

        public bool MatchName(string name)
        {
            bool commandMatchName = command.MatchName(name);
            Console.WriteLine("Comparando nome '{0}': {1}.", name, commandMatchName);

            return commandMatchName;
        }
    }
}