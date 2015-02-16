using Microsoft.AspNet.SignalR;
using Signa.Data;
using Signa.Model;
using Signa.Recognizer;

namespace Signa.Hubs
{
    public class Recognizer : Hub
    {
        private SignController signController;

        public Recognizer()
        {
            var repository = new SignRepository(SignController.SignSamplesFilePath);
            signController = new SignController(repository);
        }

        public int Recognize(SignSample data)
        {
            return Svm.Instance.Recognize(data);
        }

        public void SaveSignSample(string name, string exampleFileContent, SignSample data)
        {
            var fileName = signController.CreateSampleFileIfNotExists(name, exampleFileContent);

            signController.Add(new Sign
            {
                Description = name,
                ExampleFilePath = fileName,
                Samples = new SignSample[] { data }
            });
        }

        public SignInfo GetNextSign(int previousSignIndex)
        {
            int signIndex;
            var sign = signController.GetRandomSign(previousSignIndex, out signIndex);

            var signInfo = new SignInfo
            {
                Id = signIndex,
                Description = sign.Description,
                ExampleFilePath = sign.ExampleFilePath
            };

            return signInfo;
        }
    }
}