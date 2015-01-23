using Signa.Data;
using Signa.Recognizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.CommandLineInterface
{
    public class CommandReader
    {
        private LinkedList<ICommand> commands;
        private ICommand helpCommand;

        public CommandReader()
        {
            helpCommand = new HelpCommand();
            commands = new LinkedList<ICommand>();
            AddCommands();
        }

        private void AddCommands()
        {
            commands.AddFirst(GetLogCommand(new LoadCommand(SignSamplesController.Instance, Svm.Instance)));
        }

        private ICommand GetLogCommand(ICommand command)
        {
            return new LogCommandDecorator(command);
        }

        public void Read()
        {
            ICommand command;
            foreach (string commandName in ReadNextCommandName())
            {
                command = commands.FirstOrDefault(c => c.MatchName(commandName)) ?? helpCommand;
                command.Execute();
            }
        }

        private IEnumerable<string> ReadNextCommandName()
        {
            string line;
            while (true)
            {
                line = Console.ReadLine();
                if (line == null)
                {
                    yield break;
                }
                yield return line;
            }
        }
    }
}