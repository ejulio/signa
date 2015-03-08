using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais.Estatico;

namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(Sample sample);
        void Train(IDadosParaAlgoritmoDeReconhecimentoDeSinal data);
    }
}