using Signa.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Recognizer
{
    public class SvmTrainningData : ITrainableAlgorithmData
    {
        public double[][] Inputs { get; private set; }
        public int[] Outputs { get; private set; }
        public int ClassCount { get; private set; }

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
            int signIndex = 0;
            ClassCount = 0;
            inputs = new LinkedList<double[]>();
            outputs = new LinkedList<int>();

            foreach (var sign in signs)
            {
                ExtracSignSampleData(sign, signIndex);
                ClassCount++;
                signIndex++;
            }

            Outputs = outputs.ToArray();
            Inputs = inputs.ToArray();
        }

        private void ExtracSignSampleData(Sign sign, int signIndex)
        {
            foreach (var sample in sign.Samples)
            {
                outputs.AddFirst(signIndex);
                inputs.AddFirst(sample.Frames[0].ToArray());
            }
        }
    }
}