namespace Signa.Domain.Algorithms
{
    public interface ISignRecognitionAlgorithmFactory
    {
        IStaticSignRecognitionAlgorithm CreateStaticSignRecognizer();
    }
}