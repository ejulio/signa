using System;
using Microsoft.Owin.Hosting;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Domain;
using Signa.Domain.Algoritmos;

namespace Infraestrutura
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
            var repositoryFactory = new RepositorioFactory(SinaisEstaticosController.SignSamplesFilePath);
            var signRecognitionAlgorithmFactory = new SignRecognitionAlgorithmFactory();
            var algorithmInitializerFacade = new AlgorithmInitializerFacade(signRecognitionAlgorithmFactory, repositoryFactory);

            algorithmInitializerFacade.TrainStaticSignRecognitionAlgorithm();

            Console.WriteLine("Algoritmos treinados");
        }
    }
}
