using Signa.Domain.Algoritmos.Estatico;
using Signa.Domain.Sinais.Estatico;

namespace Signa.Domain.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(Sample sample);
        void Train(IDadosParaAlgoritmoDeReconhecimentoDeSinal data);
    }
}