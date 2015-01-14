using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Signa.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Signa.Hubs
{
    public class Recognizer : Hub
    {
        public int Recognize(SignalData data)
        {
            return SvmRecognizer.Instance.Recognize(data);
        }
    }
}