using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(Frame amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dados);
    }
}