using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Model
{
    public class SignalJsonData
    {
        public string Description { get; set; }
        public string Path { get; set; }
        public IList<SignalData> Samples { get; set; }

        public SignalJsonData()
        {
            Samples = new List<SignalData>();
        }
    }
}