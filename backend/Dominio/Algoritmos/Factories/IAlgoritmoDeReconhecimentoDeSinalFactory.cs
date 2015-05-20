using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;

namespace Dominio.Algoritmos.Factories
{
    public interface IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        IAlgoritmoClassificacaoSinaisEstaticos CriarReconhecedorDeSinaisEstaticos();
        IAlgoritmoClassificacaoSinaisDinamicos CriarReconhecedorDeSinaisDinamicos();
        IAlgoritmoClassificacaoSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos();
    }
}