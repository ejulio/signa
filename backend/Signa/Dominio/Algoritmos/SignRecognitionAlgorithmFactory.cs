using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos
{
    public class SignRecognitionAlgorithmFactory : ISignRecognitionAlgorithmFactory
    {
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos AlgoritmoDeReconhecimentoDeSinaisEstaticos;

        static SignRecognitionAlgorithmFactory()
        {
            AlgoritmoDeReconhecimentoDeSinaisEstaticos = new Svm();
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CreateStaticSignRecognizer()
        {
            return AlgoritmoDeReconhecimentoDeSinaisEstaticos;
        }
    }
}