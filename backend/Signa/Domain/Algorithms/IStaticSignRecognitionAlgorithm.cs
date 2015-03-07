using Signa.Domain.Algorithms.Static;
using Signa.Domain.Signs.Static;

namespace Signa.Domain.Algorithms
{
    public interface IStaticSignRecognitionAlgorithm
    {
        int Recognize(Sample sample);
        void Train(IDadosParaAlgoritmoDeReconhecimentoDeSinal data);
    }
}