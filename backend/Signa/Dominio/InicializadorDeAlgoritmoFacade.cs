using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;
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
            var algorithm = algoritmoDeReconhecimentoDeSinalFactory.CreateStaticSignRecognizer();
            var repository = repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos();
            var algorithmData = new DadosParaAlgoritmoDeReconhecimentoDeSinal(repository);

            algorithm.Treinar(algorithmData);
        }
    }
}