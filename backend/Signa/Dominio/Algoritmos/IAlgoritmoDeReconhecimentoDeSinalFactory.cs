namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CreateStaticSignRecognizer();
    }
}