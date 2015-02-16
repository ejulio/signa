
namespace Signa.Recognizer
{
    public interface ITrainableAlgorithmData
    {
        double[][] Inputs { get; }
        int[] Outputs { get; }
        int ClassCount { get; }
    }
}
