using Signa.Model;
using Signa.Util;
using System;
using System.IO;
using System.Linq;

namespace Signa.Data
{
    public class StaticSignController
    {
        public const string SignSamplesFilePath = "./data/sign-samples.json";
        public const string SamplesDirectory = "samples/";

        private IRepository<Sign> repository;

        public StaticSignController(IRepository<Sign> repository)
        {
            this.repository = repository;
            repository.Load();
        }

        public void Add(Sign sign)
        {
            Sign signInRepository = repository.GetById(sign.Description);
            if (signInRepository == null)
            {
                repository.Add(sign);
            }
            else
            {
                var samples = new SignSample[signInRepository.Samples.Count + sign.Samples.Count];
                
                Array.Copy(signInRepository.Samples.ToArray(), samples, signInRepository.Samples.Count);
                Array.Copy(sign.Samples.ToArray(), 0, samples, signInRepository.Samples.Count, sign.Samples.Count);
                
                signInRepository.Samples = samples;
            }
            repository.SaveChanges();
        }

        public Sign GetRandomSign(int lastSignIndex, out int signIndex)
        {
            var random = new Random();
            int index;
            do
            {
                index = random.Next(repository.Count);
            }
            while (index == lastSignIndex);

            signIndex = index;
            return repository.GetByIndex(index);
        }

        public string CreateSampleFileIfNotExists(string signDescription, string signSample)
        {
            var filePath = SamplesDirectory + signDescription.RemoveAccents().Underscore() + ".json";

            if (File.Exists(filePath))
                return filePath;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(signSample);
            }

            return filePath;
        }
    }
}