using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos.Factories
{
    public interface IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeSinaisEstaticos();
        IAlgoritmoDeReconhecimentoDeSinaisDinamicos CriarReconhecedorDeSinaisDinamicos();
        IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos();
    }
}