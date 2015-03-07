using Signa.Domain.Algorithms.Static;
using Signa.Domain.Signs.Static;

namespace Signa.Domain.Algorithms
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(Sample sample);
        void Train(IDadosParaAlgoritmoDeReconhecimentoDeSinal data);
    }
}