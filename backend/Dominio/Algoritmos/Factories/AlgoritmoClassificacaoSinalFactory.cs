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
                = new Hmm(caracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalDinamico());
        }

        public IAlgoritmoClassificacaoSinaisEstaticos CriarClassificadorSinaisEstaticos()
        {
            return algoritmoClassificacaoSinaisEstaticos;
        }

        public IAlgoritmoClassificacaoSinaisDinamicos CriarClassificadorSinaisDinamicos()
        {
            return algoritmoClassificacaoSinaisDinamicos;
        }

        public IAlgoritmoClassificacaoSinaisEstaticos CriarClassificadorFramesSinaisDinamicos()
        {
            return algoritmoClassificacaoFramesSinaisDinamicos;
        }
    }
}