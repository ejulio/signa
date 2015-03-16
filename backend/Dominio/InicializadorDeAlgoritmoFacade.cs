using System.Linq;
using Aplicacao.Dados.Repositorio;
using Aplicacao.Dominio.Algoritmos.Dados;
using Aplicacao.Dominio.Algoritmos.Factories;

namespace Aplicacao.Dominio
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
                var dadosDoAlgoritmoDeLimitesDeSinaisDinamicos = new GeradorDeDadosDosLimitesDeSinaisDinamicos(repositorio);
                algoritmoDeLimitesDeSinaisDinamicos.Treinar(dadosDoAlgoritmoDeLimitesDeSinaisDinamicos);

                var dadosDoAlgoritmo = new GeradorDeDadosDeSinaisDinamicos(repositorio);
                algoritmo.Treinar(dadosDoAlgoritmo);
            }
        }
    }
}