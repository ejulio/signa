
namespace Signa.Recognizer
{
    public interface ITrainableAlgorithm
    {
        void Train(ITrainableAlgorithmData data);
    }
}