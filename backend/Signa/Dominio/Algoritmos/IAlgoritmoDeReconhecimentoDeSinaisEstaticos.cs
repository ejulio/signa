using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(Amostra amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinal dados);
    }
}