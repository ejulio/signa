using Aplicacao;
using Dominio;
using Dominio.Algoritmos.Factories;
using Microsoft.Owin.Hosting;
using System;
using Dominio.Gerenciamento;
using Dominio.Persistencia;

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
            var repositorioFactory = new RepositorioFactory(GerenciadorSinais.CaminhoDoArquivoDoRepositorio);
            var algoritmosFactory = new AlgoritmoClassificacaoSinalFactory(new CaracteristicasFactory());
            var inicializadorDeAlgoritmosFacade = new InicializadorDeAlgoritmoFacade(algoritmosFactory, repositorioFactory);

            inicializadorDeAlgoritmosFacade.TreinarAlgoritmosClassificacaoSinais();
        }
    }
}
