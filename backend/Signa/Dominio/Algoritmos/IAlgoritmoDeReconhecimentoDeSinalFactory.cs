namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeSinaisEstaticos();
        IAlgoritmoDeReconhecimentoDeSinaisDinamicos CriarReconhecedorDeSinaisDinamicos();
    }
}