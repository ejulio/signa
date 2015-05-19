using Dominio.Algoritmos.Dados;
using Dominio.Algoritmos.Factories;
using System;
using System.Linq;
using System.Diagnostics;
using Dominio.Persistencia;

namespace Dominio
{
    public class InicializadorDeAlgoritmoFacade
    {
        private readonly IAlgoritmoDeReconhecimentoDeSinalFactory algoritmoDeReconhecimentoDeSinalFactory;
        private readonly IRepositorioFactory repositorioFactory;

        public InicializadorDeAlgoritmoFacade(IAlgoritmoDeReconhecimentoDeSinalFactory algoritmoDeReconhecimentoDeSinalFactory, IRepositorioFactory repositorioFactory)
        {
            this.algoritmoDeReconhecimentoDeSinalFactory = algoritmoDeReconhecimentoDeSinalFactory;
            this.repositorioFactory = repositorioFactory;
        }

        public void TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos()
        {
            var algoritmo = algoritmoDeReconhecimentoDeSinalFactory.CriarReconhecedorDeSinaisEstaticos();
            var repositorio = repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos();

            if (repositorio.Any())
            {
                var dadosDoAlgoritmo = new GeradorDeDadosDeSinaisEstaticos(repositorio);
                algoritmo.Treinar(dadosDoAlgoritmo);
            }
        }

        public void TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos()
        {
            var repositorio = repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos();
            
            var algoritmo = algoritmoDeReconhecimentoDeSinalFactory.CriarReconhecedorDeSinaisDinamicos();
            var algoritmoDeLimitesDeSinaisDinamicos = algoritmoDeReconhecimentoDeSinalFactory.CriarReconhecedorDeFramesDeSinaisDinamicos();

            if (repositorio.Any())
            {
                var dadosDoAlgoritmo = new GeradorDeDadosDeSinaisDinamicos(repositorio);
                algoritmo.Treinar(dadosDoAlgoritmo);

                var dadosDoAlgoritmoDeLimitesDeSinaisDinamicos = new GeradorDeDadosDosLimitesDeSinaisDinamicos(repositorio);
                algoritmoDeLimitesDeSinaisDinamicos.Treinar(dadosDoAlgoritmoDeLimitesDeSinaisDinamicos);
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