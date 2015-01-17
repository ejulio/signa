using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Signa.Data;
using Signa.Model;
using Signa.Recognizer;
using System;
using System.Collections.Generic;
using System.IO;

namespace Signa.Hubs
{
    public class Recognizer : Hub
    {
        public int Recognize(SignSample data)
        {
            return Svm.Instance.Recognize(data);
        }

        public SignInfo GetNextSign(int previousSignId)
        {
            var signId = GetRandomIndex(previousSignId);
            var sign = SignSamplesController.Instance.SignSamples[signId];

            var signInfo = new SignInfo
            {
                Id = signId,
                Description = sign.Description,
                ExampleFilePath = sign.ExampleFilePath
            };

            return signInfo;
        }

        private int GetRandomIndex(int previousSignId)
        {
            var signSamplesCount = SignSamplesController.Instance.SignSamples.Count;
            Random random = new Random();
            int signId = 0;
            do
            {
                signId = random.Next(signSamplesCount);
            } while (signId == previousSignId);
            return signId;
        }
    }
}