using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio
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
            var dadosDoAlgoritmo = new GeradorDeDadosDeSinaisEstaticos(repositorio);

            algoritmo.Treinar(dadosDoAlgoritmo);
        }

        public void TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos()
        {
            var algoritmo = algoritmoDeReconhecimentoDeSinalFactory.CriarReconhecedorDeSinaisDinamicos();
            var repositorio = repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos();
            var dadosDoAlgoritmo = new GeradorDeDadosDeSinaisDinamicos(repositorio);

            algoritmo.Treinar(dadosDoAlgoritmo);
        }
    }
}