﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Model
{
    public class Sign
    {
        public string Description { get; set; }
        public string ExampleFilePath { get; set; }
        public IList<SignSample> Samples { get; set; }

        public Sign()
        {
            Samples = new List<SignSample>();
        }
    }
}