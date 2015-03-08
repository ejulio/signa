using Signa.Domain.Algoritmos.Estatico;

namespace Signa.Domain.Algoritmos
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