
namespace Signa.Domain.Algorithms.Static
{
    public interface ISignRecognitionAlgorithmData
    {
        double[][] Inputs { get; }
        int[] Outputs { get; }
        int ClassCount { get; }
    }
}
