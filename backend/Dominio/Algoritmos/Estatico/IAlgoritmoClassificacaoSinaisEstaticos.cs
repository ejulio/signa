
using Dominio.Algoritmos.Treinamento;

namespace Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoClassificacaoSinaisEstaticos : IAlgoritmoClassificacaoSinais
    {
        void Treinar(IDadosSinaisEstaticos dados);
    }
}