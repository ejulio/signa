namespace Signa.Domain.Algorithms
{
    public interface ISignRecognitionAlgorithmFactory
    {
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CreateStaticSignRecognizer();
    }
}