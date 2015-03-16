using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;

namespace Dominio.Algoritmos.Factories
{
    public class AlgoritmoDeReconhecimentoDeSinalFactory : IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        private static IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos;
        private static IAlgoritmoDeReconhecimentoDeSinaisDinamicos algoritmoDeReconhecimentoDeSinaisDinamicos;
        private static IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeFramesDeSinaisDinamicos;

        public AlgoritmoDeReconhecimentoDeSinalFactory(IGeradorDeCaracteristicasFactory geradorDeCaracteristicasFactory)
        {
            algoritmoDeReconhecimentoDeSinaisEstaticos 
                = new Svm(geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstatico());

            algoritmoDeReconhecimentoDeFramesDeSinaisDinamicos 
                = new Svm(geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame());

            algoritmoDeReconhecimentoDeSinaisDinamicos 
                = new Hcrf(geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalDinamico());
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeSinaisEstaticos()
        {
            return algoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public IAlgoritmoDeReconhecimentoDeSinaisDinamicos CriarReconhecedorDeSinaisDinamicos()
        {
            return algoritmoDeReconhecimentoDeSinaisDinamicos;
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos()
        {
            return algoritmoDeReconhecimentoDeFramesDeSinaisDinamicos;
        }
    }
}