using Microsoft.Owin.Hosting;
using Signa.Data;
using Signa.Data.Repository;
using Signa.Domain.Algorithms;
using System;
using Signa.Domain;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();

            InitializeAlgorithms();

            Console.ReadKey();
        }

        private static void StartServer()
        {
            const string serverAddress = "http://localhost:9000";
            WebApp.Start<Signa.Startup>(serverAddress);
            Console.WriteLine("Aplicação iniciada em {0}", serverAddress);
        }

        private static void InitializeAlgorithms()
        {
            Console.WriteLine("Treinando algoritmos");
            var repositoryFactory = new RepositoryFactory(StaticSignController.SignSamplesFilePath);
            var signRecognitionAlgorithmFactory = new SignRecognitionAlgorithmFactory();
            var algorithmInitializerFacade = new AlgorithmInitializerFacade(signRecognitionAlgorithmFactory, repositoryFactory);

            algorithmInitializerFacade.TrainStaticSignRecognitionAlgorithm();

            Console.WriteLine("Algoritmos treinados");
        }
    }
}
