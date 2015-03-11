using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(Frame frame);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dados);
    }
}