
namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos : IAlgoritmoDeReconhecimentoDeSinais
    {
        void Treinar(IGeradorDeDadosDeSinaisDinamicos geradorDeDados);
    }
}