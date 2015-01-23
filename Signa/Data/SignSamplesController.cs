using Newtonsoft.Json;
using Signa.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Signa.Data
{
    public class SignSamplesController : IDataLoader
    {
        private static SignSamplesController instance;

        public static SignSamplesController Instance 
        { 
            get
            {
                if (instance == null)
                {
                    instance = new SignSamplesController();
                }
                return instance;
            }
        }

        public string SamplesFilePath { get; set; }

        public IList<Sign> Data 
        { 
            get
            {
                return signSamples;
            }
        }

        private IList<Sign> signSamples;

        private SignSamplesController()
        {
        }

        public void Load()
        {
            using (var reader = new StreamReader(SamplesFilePath))
            {
                var jsonSignSamples = reader.ReadToEnd();
                signSamples = JsonConvert.DeserializeObject<List<Sign>>(jsonSignSamples);
            }
        }
    }
}