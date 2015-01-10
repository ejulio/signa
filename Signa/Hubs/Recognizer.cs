using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Hubs
{
    public class Recognizer : Hub
    {
        static IDictionary<int, SignalParameters> parametersCollection;

        static MulticlassSupportVectorMachine svm;

        public Recognizer() : base()
        {
            if (parametersCollection == null)
            {
                parametersCollection = new Dictionary<int, SignalParameters>();
            }
        }

        public bool Recognize(SignalParameters parameters, int signalToRecognizeId)
        {
            if (svm != null)
            {
                var input = ConvertSignalParametersToArray(parameters);
                var signalRecognized = svm.Compute(input);
                return signalRecognized == signalToRecognizeId;
            }
            else
            {
                return false;
            }
        }

        public void SaveSignalParameters(SignalParameters parameters, int id)
        {
            parametersCollection.Add(id, parameters);
        }

        public void TrainRecognizer()
        {
            int[] outputs = new int[parametersCollection.Count];
            double[][] inputs = new double[parametersCollection.Count][];

            int i = 0;
            foreach (var pair in parametersCollection)
            {
                outputs[i] = pair.Key;
                inputs[i] = ConvertSignalParametersToArray(pair.Value);
                i++;
            }

            svm = new MulticlassSupportVectorMachine(0, parametersCollection.Count);

            var teacher = new MulticlassSupportVectorLearning(svm, inputs, outputs);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }
        public double[] ConvertSignalParametersToArray(SignalParameters parameters)
        {
            var array = new double[10];
            Array.Copy(parameters.PalmNormal, array, parameters.PalmNormal.Length);
            Array.Copy(parameters.HandDirection, 0, array, 3, parameters.HandDirection.Length);
            Array.Copy(parameters.AnglesBetweenFingers, 0, array, 6, parameters.AnglesBetweenFingers.Length);

            return array;
        }
    }
}