using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Microsoft.AspNet.SignalR;
using Signa.Model;
using System;
using System.Collections.Generic;

namespace Signa.Hubs
{
    public class Recognizer : Hub
    {
        static LinkedList<SignalData> data;

        public Recognizer() : base()
        {
            if (data == null)
            {
                data = new LinkedList<SignalData>();
            }
        }

        public int Recognize(SignalData data)
        {
            return SvmRecognizer.Instance.Recognize(data);
        }

        public void Save(SignalData parameters)
        {
            data.AddFirst(parameters);
        }

        public void Train()
        {
            SvmRecognizer.Instance.Train(data);
        }
    }
}