using Aplicacao.Dominio.Algoritmos.Dinamico;
using Aplicacao.Dominio.Algoritmos.Estatico;

namespace Aplicacao.Dominio.Algoritmos.Factories
{
    public interface IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeSinaisEstaticos();
        IAlgoritmoDeReconhecimentoDeSinaisDinamicos CriarReconhecedorDeSinaisDinamicos();
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos();
    }
}