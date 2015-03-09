using Microsoft.AspNet.SignalR;
using Signa.Dominio.Sinais;
using System;

namespace Signa.Hubs
{
    public class ReconhecedorDeSinaisDinamicos : Hub
    {
        public int Recognize(Frame[] framesDeSinal)
        {
            throw new NotImplementedException("Implementar para reconhecer o sinal utilizando HMM ou HCRF");
        }

        public int RecognizeFirstFrame(Frame frameDeSinal)
        {
            throw new NotImplementedException("Implemetar para reconhecer o frame utilizando SVM");
        }

        public int RecognizeLastFrame(Frame frameDeSinal)
        {
            throw new NotImplementedException("Implemetar para reconhecer o frame utilizando SVM");
        }

        public void Save(string name, string exampleFileContent, Frame[] framesDeSinal)
        {
            throw new NotImplementedException("Implementar para salvar o sinal e treinar o algoritmo HMM ou HCRF");
        }
    }
}