using Signa.Data;
using Signa.Recognizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.CommandLineInterface
{
    public class LoadCommand : ICommand
    {
        ITrainableAlgorithm algorithm;
        IDataLoader dataLoader;

        public LoadCommand(IDataLoader dataLoader, ITrainableAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            this.dataLoader = dataLoader;
        }

        public void Execute()
        {
            dataLoader.Load();
            var data = new SvmTrainningData(dataLoader.Data);
            algorithm.Train(data);
        }

        public bool MatchName(string name)
        {
            return name == "carregar-exemplos";
        }
    }
}