using Signa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Signa.Recognizer
{
    public interface ITrainableAlgorithmData
    {
        double[][] Inputs { get; }
        int[] Outputs { get; }
        int ClassCount { get; }
    }
}
