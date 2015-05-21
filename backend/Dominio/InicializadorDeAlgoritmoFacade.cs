using Dominio.Algoritmos.Factories;
using Dominio.Algoritmos.Treinamento;
using Dominio.Persistencia;
using System;
using System.Diagnostics;
using System.Linq;

namespace Dominio
{
    public class InicializadorDeAlgoritmoFacade
    {
        private readonly IAlgoritmoClassificacaoSinalFactory algoritmoClassificacaoSinalFactory;
        private readonly IRepositorioFactory repositorioFactory;

        public InicializadorDeAlgoritmoFacade(IAlgoritmoClassificacaoSinalFactory algoritmoClassificacaoSinalFactory, IRepositorioFactory repositorioFactory)
        {
            this.algoritmoClassificacaoSinalFactory = algoritmoClassificacaoSinalFactory;
            this.repositorioFactory = repositorioFactory;
        }

        public void TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos()
        {
            var algoritmo = algoritmoClassificacaoSinalFactory.CriarReconhecedorDeSinaisEstaticos();
            var repositorio = repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos();

            if (repositorio.Any())
            {
                var dadosSinaisEstaticos = new DadosSinaisEstaticos(repositorio);
                dadosSinaisEstaticos.Processar();
                algoritmo.Treinar(dadosSinaisEstaticos);
            }
        }

        public void TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos()
        {
            var repositorio = repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos();
            
            var algoritmo = algoritmoClassificacaoSinalFactory.CriarReconhecedorDeSinaisDinamicos();
            var algoritmoDeLimitesDeSinaisDinamicos = algoritmoClassificacaoSinalFactory.CriarReconhecedorDeFramesDeSinaisDinamicos();

            if (repositorio.Any())
            {
                var dadosSinaisDinamicos = new DadosSinaisDinamicos(repositorio);
                dadosSinaisDinamicos.Processar();
                algoritmo.Treinar(dadosSinaisDinamicos);

                var dadosFramesSinaisDinamicos = new DadosFramesSinaisDinamicos(repositorio);
                dadosFramesSinaisDinamicos.Processar();
                algoritmoDeLimitesDeSinaisDinamicos.Treinar(dadosFramesSinaisDinamicos);
            }
        }

        public void TreinarAlgoritmosDeReconhecimentoDeSinais()
        {
            Console.WriteLine("Treinando algoritmos...");
            
            var stopwatchTempoTotal = Stopwatch.StartNew();

            var stopwatchSinaisEstaticos = Stopwatch.StartNew();
            TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();
            stopwatchSinaisEstaticos.Stop();
            Console.WriteLine("Tempo para treinar o reconhecimento de sinais estáticos foi {0}ms", stopwatchSinaisEstaticos.ElapsedMilliseconds);

            var stopwatchSinaisDinamicos = Stopwatch.StartNew();
            TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos();
            stopwatchSinaisDinamicos.Stop();
            Console.WriteLine("Tempo para treinar o reconhecimento de sinais dinâmicos foi {0}ms", stopwatchSinaisDinamicos.ElapsedMilliseconds);

            stopwatchTempoTotal.Stop();
            
            Console.WriteLine("Fim do treinamento de algoritmos.");
            Console.WriteLine("Tempo total para treinar os algoritmos foi {0}ms", stopwatchTempoTotal.ElapsedMilliseconds);
        }
    }
}