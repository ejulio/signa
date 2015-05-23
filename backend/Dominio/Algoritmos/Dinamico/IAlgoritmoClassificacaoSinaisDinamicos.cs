
using Dominio.Algoritmos.Treinamento;

namespace Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoClassificacaoSinaisDinamicos : IAlgoritmoClassificacaoSinais
    {
        void Aprender(IDadosSinaisDinamicos dados);
    }
}