using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;

namespace Dominio.Algoritmos.Factories
{
    public interface IAlgoritmoClassificacaoSinalFactory
    {
        IAlgoritmoClassificacaoSinaisEstaticos CriarClassificadorSinaisEstaticos();
        IAlgoritmoClassificacaoSinaisDinamicos CriarClassificadorSinaisDinamicos();
        IAlgoritmoClassificacaoSinaisEstaticos CriarClassificadorFramesSinaisDinamicos();
    }
}