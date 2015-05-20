using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;

namespace Dominio.Algoritmos.Factories
{
    public class AlgoritmoDeReconhecimentoDeSinalFactory : IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        private static IAlgoritmoClassificacaoSinaisEstaticos algoritmoClassificacaoSinaisEstaticos;
        private static IAlgoritmoClassificacaoSinaisDinamicos algoritmoClassificacaoSinaisDinamicos;
        private static IAlgoritmoClassificacaoSinaisEstaticos algoritmoClassificacaoFramesSinaisDinamicos;

        public AlgoritmoDeReconhecimentoDeSinalFactory(IGeradorDeCaracteristicasFactory geradorDeCaracteristicasFactory)
        {
            algoritmoClassificacaoSinaisEstaticos 
                = new Svm(geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstatico());

            algoritmoClassificacaoFramesSinaisDinamicos 
                = new Svm(geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame());

            algoritmoClassificacaoSinaisDinamicos 
                = new Hcrf(geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalDinamico());
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