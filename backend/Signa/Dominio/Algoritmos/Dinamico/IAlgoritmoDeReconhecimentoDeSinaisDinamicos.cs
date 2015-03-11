using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos
    {
        int Reconhecer(Frame[] amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisDinamicos dados);
    }
}