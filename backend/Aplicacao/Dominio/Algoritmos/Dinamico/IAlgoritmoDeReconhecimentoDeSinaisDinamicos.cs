
using Signa.Dominio.Algoritmos.Dados;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos : IAlgoritmoDeReconhecimentoDeSinais
    {
        void Treinar(IGeradorDeDadosDeSinaisDinamicos geradorDeDados);
    }
}