using Microsoft.AspNet.SignalR;
using Signa.Data;
using Signa.Model;
using System;

namespace Signa.Hubs
{
    public class Sign : Hub
    {
        private StaticSignController staticSignController;

        public Sign(IRepository<Model.Sign> repository)
        {
            staticSignController = new StaticSignController(repository);
        }

        public int Recognize(SignFrame data)
        {
            throw new NotImplementedException("Implementar nos devidos hubs");
            //return Svm.Instance.Recognize(data);
        }

        public void SaveSignSample(string name, string exampleFileContent, SignSample data)
        {
            throw new NotImplementedException("implementar nos devidos hubs");
            var fileName = staticSignController.CreateSampleFileIfNotExists(name, exampleFileContent);

            staticSignController.Add(new Model.Sign
            {
                Description = name,
                ExampleFilePath = fileName,
                Samples = new[] { data }
            });
        }

        public SignInfo GetNextSign(int previousSignIndex)
        {
            // alterar para receber os dados dos 2 repositórios
            int signIndex;
            var sign = staticSignController.GetRandomSign(previousSignIndex, out signIndex);

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