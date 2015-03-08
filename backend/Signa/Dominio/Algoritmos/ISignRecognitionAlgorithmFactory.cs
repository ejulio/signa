namespace Signa.Dominio.Algoritmos
{
    public interface ISignRecognitionAlgorithmFactory
    {
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CreateStaticSignRecognizer();
    }
}