using Newtonsoft.Json;
using Signa.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Signa
{
    public class SignSamplesController
    {
        public static IList<Sign> SignSamples { get; set; }

        private string samplesFilePath;

        public SignSamplesController(string samplesFilePath)
        {
            this.samplesFilePath = samplesFilePath;
        }

        public void Load()
        {
            using (var reader = new StreamReader(samplesFilePath))
            {
                var jsonSignSamples = reader.ReadToEnd();
                SignSamples = JsonConvert.DeserializeObject<List<Sign>>(jsonSignSamples);
            }
        }
    }
}