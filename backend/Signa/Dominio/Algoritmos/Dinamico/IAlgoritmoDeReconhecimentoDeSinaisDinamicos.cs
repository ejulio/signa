using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos
    {
        int Reconhecer(IAmostraDeSinalDinamico amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisDinamicos dados);
    }
}