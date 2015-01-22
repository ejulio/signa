using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Signa.Recognizer
{
    public interface ITrainableAlgorithmData
    {
        double[][] Inputs { get; set; }
        int[] Outputs { get; set; }
        int ClassCount { get; set; }
    }
}
