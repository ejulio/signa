using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Newtonsoft.Json;
using Signa.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Signa
{
    public class SvmRecognizer
    {
        private static SvmRecognizer instance;

        public static SvmRecognizer Instance 
        { 
            get
            {
                if (instance == null)
                {
                    instance = new SvmRecognizer();
                }

                return instance;
            }
        }

        private MulticlassSupportVectorMachine svm;

        private SvmRecognizer()
        {

        }

        public int Recognize(SignSample data)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            return svm.Compute(data.ToArray());
        }

        public void Train(SvmRecognizerTrainningData data)
        {
            svm = new MulticlassSupportVectorMachine(0, data.ClassCount);

            var teacher = new MulticlassSupportVectorLearning(svm, data.Inputs, data.Outputs);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => 
                                    new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }
    }
}