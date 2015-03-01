using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Signa.Model;
using System;

namespace Signa.Recognizer
{
    public class Svm : ITrainableAlgorithm
    {
        private static Svm instance;

        public static Svm Instance 
        { 
            get
            {
                if (instance == null)
                {
                    instance = new Svm();
                }

                return instance;
            }
        }

        private MulticlassSupportVectorMachine svm;

        private Svm()
        {

        }

        public int Recognize(SignFrame data)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            return svm.Compute(data.ToArray());
        }

        public void Train(ITrainableAlgorithmData data)
        {
            svm = new MulticlassSupportVectorMachine(0, data.ClassCount);

            var teacher = new MulticlassSupportVectorLearning(svm, data.Inputs, data.Outputs);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => 
                                    new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }
    }
}