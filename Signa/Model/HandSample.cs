﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Model
{
    public class HandSample
    {
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }
        public double[] AnglesBetweenFingers { get; set; }

        public double[] ToArray()
        {
            var array = new double[10];

            PalmNormal.CopyTo(array, 0);
            HandDirection.CopyTo(array, PalmNormal.Length);
            AnglesBetweenFingers.CopyTo(array, PalmNormal.Length + HandDirection.Length);

            return array;
        }
        public static HandSample DefaultSample()
        {
            return new HandSample
            {
                AnglesBetweenFingers = new[] { 0.0, 0.0, 0.0, 0.0 },
                HandDirection = new[] { 0.0, 0.0, 0.0 },
                PalmNormal = new[] { 0.0, 0.0, 0.0 }
            };
        }

    }
}