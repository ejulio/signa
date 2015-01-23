using Signa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Recognizer
{
    public class SvmTrainningData : ITrainableAlgorithmData
    {
        public double[][] Inputs { get; set; }
        public int[] Outputs { get; set; }
        public int ClassCount { get; set; }

        private IEnumerable<Sign> signs;
        private LinkedList<int> outputs;
        private LinkedList<double[]> inputs;

        public SvmTrainningData(IEnumerable<Sign> signs)
        {
            this.signs = signs;
            Process();
        }

        public void Process()
        {
            int signId = 0;
            ClassCount = 0;
            inputs = new LinkedList<double[]>();
            outputs = new LinkedList<int>();

            foreach (var sign in signs)
            {
                ExtracSignSampleData(sign, signId);
                ClassCount++;
                signId++;
            }

            Outputs = outputs.ToArray();
            Inputs = inputs.ToArray();
        }

        private void ExtracSignSampleData(Sign sign, int signId)
        {
            foreach (var sample in sign.Samples)
            {
                outputs.AddFirst(signId);
                inputs.AddFirst(sample.ToArray());
            }
        }
    }
}