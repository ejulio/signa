using Signa.Domain.Algorithms.Static;

namespace Signa.Domain.Algorithms
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