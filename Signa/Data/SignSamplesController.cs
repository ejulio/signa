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

        private IDictionary<string, Sign> signIndexes;

        private SignSamplesController()
        {
            signIndexes = new Dictionary<string, Sign>();
        }

        public void Load()
        {
            using (var reader = new StreamReader(SamplesFilePath))
            {
                var jsonSignSamples = reader.ReadToEnd();
                signSamples = JsonConvert.DeserializeObject<List<Sign>>(jsonSignSamples);
                LoadDictionary();
            }
        }

        private void LoadDictionary()
        {
            foreach (var sign in signSamples)
            {
                signIndexes.Add(sign.Description, sign);
            }
        }

        public void Add(Sign sign)
        {
            Sign signInIndex;
            if (signIndexes.TryGetValue(sign.Description, out signInIndex))
            {
                foreach (var sample in sign.Samples)
                {
                    var samples = new SignSample[signInIndex.Samples.Count + sign.Samples.Count];
                    Array.Copy(signInIndex.Samples.ToArray(), samples, signInIndex.Samples.Count);
                    Array.Copy(sign.Samples.ToArray(), 0, samples, signInIndex.Samples.Count, sign.Samples.Count);
                    
                    signInIndex.Samples = samples;
                }
            }
            else
            {
                signIndexes.Add(sign.Description, sign);
            }
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(SamplesFilePath))
            {
                var json = JsonConvert.SerializeObject(signIndexes.Values);
                writer.Write(json);
            }
        }
    }
}