
using Dominio.Algoritmos.Dados;

namespace Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoClassificacaoSinaisDinamicos : IAlgoritmoClassificacaoSinais
    {
        void Treinar(IGeradorDeDadosDeSinaisDinamicos geradorDeDados);
    }
}