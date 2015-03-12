using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos
{
    public class AlgoritmoDeReconhecimentoDeSinalFactory : IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos AlgoritmoDeReconhecimentoDeSinaisEstaticos;
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisDinamicos AlgoritmoDeReconhecimentoDeSinaisDinamicos;
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos AlgoritmoDeReconhecimentoDeFramesDeSinaisDinamicos;

        static AlgoritmoDeReconhecimentoDeSinalFactory()
        {
            var geradorDeCaracteristicasDeSinalDinamico = new GeradorDeCaracteristicasDeSinalDinamico();
            var geradorDeCaracteristicasDeSinalEstatico = new GeradorDeCaracteristicasDeSinalEstatico();
            var geradorDeCaracteristicasDeSinalEstaticoComTipoFrame = 
                new GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(geradorDeCaracteristicasDeSinalEstatico);

            AlgoritmoDeReconhecimentoDeSinaisEstaticos = new Svm(geradorDeCaracteristicasDeSinalEstatico);
            AlgoritmoDeReconhecimentoDeFramesDeSinaisDinamicos = new Svm(geradorDeCaracteristicasDeSinalEstaticoComTipoFrame);
            AlgoritmoDeReconhecimentoDeSinaisDinamicos = new Hcrf(geradorDeCaracteristicasDeSinalDinamico);
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeSinaisEstaticos()
        {
            return AlgoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public IAlgoritmoDeReconhecimentoDeSinaisDinamicos CriarReconhecedorDeSinaisDinamicos()
        {
            return AlgoritmoDeReconhecimentoDeSinaisDinamicos;
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos()
        {
            return AlgoritmoDeReconhecimentoDeFramesDeSinaisDinamicos;
        }
    }
}