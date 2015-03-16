using Microsoft.Owin.Hosting;
using System;
using Aplicacao;
using Aplicacao.Dados;
using Aplicacao.Dados.Repositorio;
using Aplicacao.Dominio;
using Aplicacao.Dominio.Algoritmos.Factories;

namespace Infraestrutura
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();

            TreinarAlgoritmos();

            Console.ReadKey();
        }

        private static void StartServer()
        {
            const string serverAddress = "http://localhost:9000";
            WebApp.Start<Startup>(serverAddress);
            Console.WriteLine("Aplicação iniciada em {0}", serverAddress);
        }

        private static void TreinarAlgoritmos()
        {
            Console.WriteLine("Treinando algoritmos");
            var repositorioFactory = new RepositorioFactory(SinaisController.CaminhoDoArquivoDoRepositorio);
            var algoritmosFactory = new AlgoritmoDeReconhecimentoDeSinalFactory(new GeradorDeCaracteristicasFactory());
            var inicializadorDeAlgoritmosFacade = new InicializadorDeAlgoritmoFacade(algoritmosFactory, repositorioFactory);

            inicializadorDeAlgoritmosFacade.TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();
            inicializadorDeAlgoritmosFacade.TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos();

            Console.WriteLine("Algoritmos treinados");
        }
    }
}
