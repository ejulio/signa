using Aplicacao;
using Dominio;
using Dominio.Algoritmos.Factories;
using Dominio.Dados;
using Dominio.Dados.Repositorio;
using Microsoft.Owin.Hosting;
using System;

namespace Infraestrutura
{
    class Program
    {
        static void Main(string[] args)
        {
            IniciarServidor();

            TreinarAlgoritmos();

            Console.ReadKey();
        }

        private static void IniciarServidor()
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
