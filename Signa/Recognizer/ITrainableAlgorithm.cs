using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Recognizer
{
    public interface ITrainableAlgorithm
    {
        void Train(ITrainableAlgorithmData data);
    }
}