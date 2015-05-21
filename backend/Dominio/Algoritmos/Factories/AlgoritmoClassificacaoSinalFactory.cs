using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;

namespace Dominio.Algoritmos.Factories
{
    public class AlgoritmoClassificacaoSinalFactory : IAlgoritmoClassificacaoSinalFactory
    {
        private static IAlgoritmoClassificacaoSinaisEstaticos algoritmoClassificacaoSinaisEstaticos;
        private static IAlgoritmoClassificacaoSinaisDinamicos algoritmoClassificacaoSinaisDinamicos;
        private static IAlgoritmoClassificacaoSinaisEstaticos algoritmoClassificacaoFramesSinaisDinamicos;

        public AlgoritmoClassificacaoSinalFactory(ICaracteristicasFactory caracteristicasFactory)
        {
            algoritmoClassificacaoSinaisEstaticos 
                = new Svm(caracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstatico());

            algoritmoClassificacaoFramesSinaisDinamicos 
                = new Svm(caracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame());

            algoritmoClassificacaoSinaisDinamicos 
                = new Hcrf(caracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalDinamico());
        }

        public IAlgoritmoClassificacaoSinaisEstaticos CriarReconhecedorDeSinaisEstaticos()
        {
            return algoritmoClassificacaoSinaisEstaticos;
        }

        public IAlgoritmoClassificacaoSinaisDinamicos CriarReconhecedorDeSinaisDinamicos()
        {
            return algoritmoClassificacaoSinaisDinamicos;
        }

        public IAlgoritmoClassificacaoSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos()
        {
            return algoritmoClassificacaoFramesSinaisDinamicos;
        }
    }
}