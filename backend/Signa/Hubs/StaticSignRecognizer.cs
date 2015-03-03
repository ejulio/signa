using Microsoft.AspNet.SignalR;
using Signa.Data;
using Signa.Domain.Signs.Static;
using System;

namespace Signa.Hubs
{
    public class StaticSignRecognizer : Hub
    {
        private readonly StaticSignController signController;

        public StaticSignRecognizer(StaticSignController signController)
        {
            this.signController = signController;
        }

        public int Recognize(Sample sample)
        {
            return signController.Recognize(sample);
        }

        public void Save(string name, string exampleFileContent, Sample sample)
        {
            //throw new NotImplementedException("Mover implementação para StaticSignController");
            var fileName = signController.CreateSampleFileIfNotExists(name, exampleFileContent);
            signController.Add(new Sign
            {
                Description = name,
                ExampleFilePath = fileName,
                Samples = new[] { sample }
            });
        }
    }
}