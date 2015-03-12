using Microsoft.Owin.Hosting;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Dominio;
using Signa.Dominio.Algoritmos;
using System;

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
            var repositoryFactory = new RepositorioFactory(SinaisController.CaminhoDoArquivoDoRepositorio);
            var signRecognitionAlgorithmFactory = new AlgoritmoDeReconhecimentoDeSinalFactory(new GeradorDeCaracteristicasFactory());
            var algorithmInitializerFacade = new InicializadorDeAlgoritmoFacade(signRecognitionAlgorithmFactory, repositoryFactory);

            algorithmInitializerFacade.TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();

            Console.WriteLine("Algoritmos treinados");
        }
    }
}
