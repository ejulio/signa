
using Dominio.Algoritmos.Dados;

namespace Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoClassificacaoSinaisEstaticos : IAlgoritmoClassificacaoSinais
    {
        void Treinar(IDadosSinaisEstaticos dados);
    }
}