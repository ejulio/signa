namespace Signa.Domain.Algorithms
{
    public class SignRecognitionAlgorithmFactory
    {
        private static readonly IStaticSignRecognitionAlgorithm StaticSignRecognitionAlgorithm;

        static SignRecognitionAlgorithmFactory()
        {
            StaticSignRecognitionAlgorithm = new Svm();
        }

        public IStaticSignRecognitionAlgorithm CreateStaticSignRecognizer()
        {
            return StaticSignRecognitionAlgorithm;
        }
    }
}