namespace Signa.Domain.Algoritmos
{
    public interface ISignRecognitionAlgorithmFactory
    {
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CreateStaticSignRecognizer();
    }
}