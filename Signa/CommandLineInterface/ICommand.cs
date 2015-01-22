using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.CommandLineInterface
{
    public interface ICommand
    {
        void Execute();
        bool MatchName(string name);
    }
}