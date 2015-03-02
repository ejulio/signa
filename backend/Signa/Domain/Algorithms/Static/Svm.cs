using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Signa.Domain.Signs.Static;
using System;

namespace Signa.Domain.Algorithms.Static
{
    public class Svm : IStaticSignRecognitionAlgorithm
    {
        private MulticlassSupportVectorMachine svm;

        public int Recognize(Sample data)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            return svm.Compute(data.ToArray());
        }

        public void Train(ISignRecognitionAlgorithmData data)
        {
            svm = new MulticlassSupportVectorMachine(0, data.ClassCount);

            var teacher = new MulticlassSupportVectorLearning(svm, data.Inputs, data.Outputs);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => 
                                    new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }
    }
}