using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(IAmostraDeSinalEstatico amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dados);
    }
}