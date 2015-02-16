using Microsoft.AspNet.SignalR;
using Signa.Data;
using Signa.Model;
using Signa.Recognizer;
using System;
using System.IO;

namespace Signa.Hubs
{
    public class Recognizer : Hub
    {
        public int Recognize(SignSample data)
        {
            return Svm.Instance.Recognize(data);
        }

        public void SaveSignSample(string name, string exampleFileContent, SignSample data)
        {
            if (!Directory.Exists("samples"))
            {
                Directory.CreateDirectory("samples");
            }

            var fileName = "samples/" + name + ".json";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(exampleFileContent);
            }

            SignSamplesController.Instance.Add(new Sign
            {
                Description= name,
                ExampleFilePath = fileName,
                Samples = new SignSample[] { data }
            });

            SignSamplesController.Instance.Save();
        }

        public SignInfo GetNextSign(int previousSignId)
        {
            var signId = GetRandomIndex(previousSignId);
            var sign = SignSamplesController.Instance.GetByIndex(signId);

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
            var signSamplesCount = SignSamplesController.Instance.Count;
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