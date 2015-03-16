
using Aplicacao.Dominio.Algoritmos.Dados;

namespace Aplicacao.Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos : IAlgoritmoDeReconhecimentoDeSinais
    {
        void Treinar(IGeradorDeDadosDeSinaisDinamicos geradorDeDados);
    }
}