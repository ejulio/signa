using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;

namespace Dominio.Algoritmos.Factories
{
    public interface IAlgoritmoClassificacaoSinalFactory
    {
        IAlgoritmoClassificacaoSinaisEstaticos CriarReconhecedorDeSinaisEstaticos();
        IAlgoritmoClassificacaoSinaisDinamicos CriarReconhecedorDeSinaisDinamicos();
        IAlgoritmoClassificacaoSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos();
    }
}