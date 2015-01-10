using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa
{
    public class SignalParameters
    {
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }
        public double[] AnglesBetweenFingers { get; set; }
    }
}