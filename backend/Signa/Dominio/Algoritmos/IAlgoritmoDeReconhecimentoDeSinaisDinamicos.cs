using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos
    {
        int Reconhecer(IAmostraDeSinalDinamico amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinal dados);
    }
}