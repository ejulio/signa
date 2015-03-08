using Microsoft.AspNet.SignalR;
using Signa.Domain.Signs.Dynamic;
using System;

namespace Signa.Hubs
{
    public class DynamicSignRecognizer : Hub
    {
        public int Recognize(FrameDeSinal[] framesDeSinal)
        {
            throw new NotImplementedException("Implementar para reconhecer o sinal utilizando HMM ou HCRF");
        }

        public int RecognizeFirstFrame(FrameDeSinal frameDeSinal)
        {
            throw new NotImplementedException("Implemetar para reconhecer o frame utilizando SVM");
        }

        public int RecognizeLastFrame(FrameDeSinal frameDeSinal)
        {
            throw new NotImplementedException("Implemetar para reconhecer o frame utilizando SVM");
        }

        public void Save(string name, string exampleFileContent, FrameDeSinal[] framesDeSinal)
        {
            throw new NotImplementedException("Implementar para salvar o sinal e treinar o algoritmo HMM ou HCRF");
        }
    }
}