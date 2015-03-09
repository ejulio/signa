using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(IAmostraDeSinalEstatico amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dados);
    }
}