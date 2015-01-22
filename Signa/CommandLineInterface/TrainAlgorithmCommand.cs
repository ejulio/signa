using Signa.Recognizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.CommandLineInterface
{
    public class TrainAlgorithmCommand : ICommand
    {
        ITrainableAlgorithm algorithm;
        ITrainableAlgorithmData data;

        public TrainAlgorithmCommand(ITrainableAlgorithm algorithm, ITrainableAlgorithmData data)
        {
            this.algorithm = algorithm;
            this.data = data;
        }

        public void Execute()
        {
            algorithm.Train(data);
        }

        public bool MatchName(string name)
        {
            return name == "treinar";
        }
    }
}