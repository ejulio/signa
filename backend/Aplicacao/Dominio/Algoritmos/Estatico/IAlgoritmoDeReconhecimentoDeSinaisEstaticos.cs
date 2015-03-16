
using Aplicacao.Dominio.Algoritmos.Dados;

namespace Aplicacao.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos : IAlgoritmoDeReconhecimentoDeSinais
    {
        void Treinar(IGeradorDeDadosDeSinaisEstaticos dados);
    }
}