using Microsoft.Owin.Hosting;
using Signa.Data;
using Signa.Data.Repository;
using Signa.Domain.Algorithms;
using Signa.Domain.Algorithms.Static;
using System;

namespace Host
{
    class Program
    {
        [Obsolete("MELHORAR O CÓDIGO DE INICIALIZAÇÃO")]
        static void Main(string[] args)
        {
            Console.WriteLine("MELHORAR O CÓDIGO DE INICIALIZAÇÃO");
            StartServer();

            var repository = new StaticSignRepository(StaticSignController.SignSamplesFilePath);
            repository.Load();
            if (repository.Count == 0)
            {
                Console.WriteLine("Sem dados para treinar o algoritmo");
            }
            else
            {
                Console.WriteLine("Treinando o algoritmo com os exemplos");
                var trainningData = new SignRecognitionAlgorithmData(repository);
                new SignRecognitionAlgorithmFactory().CreateStaticSignRecognizer().Train(trainningData);
                Console.WriteLine("Algoritmo treinado");
            }

            Console.ReadKey();
        }

        private static void StartServer()
        {
            var serverAddress = "http://localhost:9000";
            WebApp.Start<Signa.Startup>(serverAddress);
            Console.WriteLine("Aplicação iniciada em {0}", serverAddress);
        }
    }
}
