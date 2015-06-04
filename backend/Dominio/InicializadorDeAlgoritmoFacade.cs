using Dominio.Algoritmos.Factories;
using Dominio.Algoritmos.Treinamento;
using Dominio.Persistencia;
using System;
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
                using (new MedidorTempo("montar dados de treinamento de sinais estáticos."))
                    dadosSinaisEstaticos.Processar();

                using (new MedidorTempo("treinar algoritmos de sinais estáticos."))
                    algoritmo.Aprender(dadosSinaisEstaticos);
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
                using (new MedidorTempo("montar dados de treinamento de sinais dinâmicos."))
                    dadosSinaisDinamicos.Processar();

                using (new MedidorTempo("treinar algoritmo de sinais dinâmicos."))
                    algoritmo.Aprender(dadosSinaisDinamicos);

                var dadosFramesSinaisDinamicos = new DadosFramesSinaisDinamicos(repositorio);
                using (new MedidorTempo("montar dados de treinamento de frames de sinais dinâmicos."))
                    dadosFramesSinaisDinamicos.Processar();

                using (new MedidorTempo("treinar algoritmos de frames sinais dinâmicos."))
                    algoritmoDeLimitesDeSinaisDinamicos.Aprender(dadosFramesSinaisDinamicos);
            }
        }

        public void TreinarAlgoritmosDeReconhecimentoDeSinais()
        {
            Console.WriteLine("Treinando algoritmos...");
            
            using (new MedidorTempo("tempo total algoritmos."))
            {
                using (new MedidorTempo("tempo total algoritmos de sinais estáticos."))
                    TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();

                using (new MedidorTempo("tempo total algoritmos de sinais dinâmicos."))
                    TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos();
            }
            
            Console.WriteLine("Fim do treinamento de algoritmos.");
        }
    }
}