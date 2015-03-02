using Microsoft.AspNet.SignalR;
using Signa.Data;
using Signa.Domain.Signs.Dynamic;
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

        public int Recognize(SignFrame frame)
        {
            throw new NotImplementedException("Implementar para retornar o resultado do reconhecimento via SVM");
        }

        public void Save(string name, string exampleFileContent, SignFrame frame)
        {
            throw new NotImplementedException("Implementar para salvar no repositório e treinar o algoritmo SVM");
        }
    }
}