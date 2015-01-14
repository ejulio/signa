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

        public int Recognize(SignalData data)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            return svm.Compute(data.ToInputArray());
        }

        public void Train(IEnumerable<SignalData> data)
        {
            int dataCount = data.Count();
            int classCount = 0;
            int lastClass = -1;
            int[] outputs = new int[dataCount];
            double[][] inputs = new double[dataCount][];

            int i = 0;
            foreach (var signalData in data)
            {
                if (signalData.Id != lastClass)
                {
                    lastClass = signalData.Id;
                    classCount++;
                }
                outputs[i] = signalData.Id;
                inputs[i] = signalData.ToInputArray();
                i++;
            }

            svm = new MulticlassSupportVectorMachine(0, classCount);

            var teacher = new MulticlassSupportVectorLearning(svm, inputs, outputs);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => 
                                    new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }

        public void TrainFromFile()
        {
            using (var sr = new StreamReader("./data/signal-examples.json"))
            {
                var jsonData = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<List<SignalJsonData>>(jsonData);

                var signalDataList = new LinkedList<SignalData>();

                for (int i = 0; i < data.Count; i++)
                {
                    foreach (var signalData in data[i].Samples)
                    {
                        signalData.Id = i;
                        signalDataList.AddFirst(signalData);
                    }
                }

                Train(signalDataList);
            }
        }
    }
}