using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos
{
    public class AlgoritmoDeReconhecimentoDeSinalFactory : IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos AlgoritmoDeReconhecimentoDeSinaisEstaticos;

        static AlgoritmoDeReconhecimentoDeSinalFactory()
        {
            AlgoritmoDeReconhecimentoDeSinaisEstaticos = new Svm();
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CreateStaticSignRecognizer()
        {
            return AlgoritmoDeReconhecimentoDeSinaisEstaticos;
        }
    }
}