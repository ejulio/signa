
namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos : IAlgoritmoDeReconhecimentoDeSinais
    {
        void Treinar(IGeradorDeDadosDeSinaisEstaticos dados);
    }
}