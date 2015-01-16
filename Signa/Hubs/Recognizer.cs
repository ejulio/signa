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
        public int Recognize(SignSample data)
        {
            return SvmRecognizer.Instance.Recognize(data);
        }

        public SignInfo GetNextSign(int previousSignId)
        {
            var signSamplesCount = SignSamplesController.SignSamples.Count;
            Random random = new Random();
            var index = random.Next(signSamplesCount);

            var sign = SignSamplesController.SignSamples[index];

            var signInfo = new SignInfo
            {
                Id = index,
                Description = sign.Description,
                ExampleFilePath = sign.ExampleFilePath
            };

            return signInfo;
        }
    }
}