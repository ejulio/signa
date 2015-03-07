using Microsoft.AspNet.SignalR;
using Signa.Dados;
using Signa.Domain.Signs.Static;

namespace Signa.Hubs
{
    public class StaticSignRecognizer : Hub
    {
        private readonly SinaisEstaticosController signController;

        public StaticSignRecognizer(SinaisEstaticosController signController)
        {
            this.signController = signController;
        }

        public int Recognize(Sample sample)
        {
            return signController.Reconhecer(sample);
        }

        public void Save(string name, string exampleFileContent, Sample sample)
        {
            signController.Save(name, exampleFileContent, sample);
        }
    }
}