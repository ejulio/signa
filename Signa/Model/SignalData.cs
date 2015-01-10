﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Model
{
    public class SignalData
    {
        public int Id { get; set; }
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }
        public double[] AnglesBetweenFingers { get; set; }

        public double[] ToInputArray()
        {
            var array = new double[10];
            
            PalmNormal.CopyTo(array, 0);
            HandDirection.CopyTo(array, PalmNormal.Length);
            AnglesBetweenFingers.CopyTo(array, PalmNormal.Length + HandDirection.Length);

            return array;
        }
    }
}