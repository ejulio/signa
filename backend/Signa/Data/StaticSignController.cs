using Signa.Data.Repository;
using Signa.Domain.Algorithms;
using Signa.Domain.Signs.Static;
using Signa.Util;
using System;
using System.IO;
using System.Linq;

namespace Signa.Data
{
    public class StaticSignController
    {
        public const string SignSamplesFilePath = "./data/static-sign-samples.json";

        public const string SamplesDirectory = "samples/"; 

        private IRepository<Sign> repository;
        private readonly IStaticSignRecognitionAlgorithm staticSignRecognitionAlgorithm;

        public StaticSignController(IRepository<Sign> repository, IStaticSignRecognitionAlgorithm staticSignRecognitionAlgorithm)
        {
            this.repository = repository;
            this.staticSignRecognitionAlgorithm = staticSignRecognitionAlgorithm;
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
                var samples = new Sample[signInRepository.Samples.Count + sign.Samples.Count];
                
                Array.Copy(signInRepository.Samples.ToArray(), samples, signInRepository.Samples.Count);
                Array.Copy(sign.Samples.ToArray(), 0, samples, signInRepository.Samples.Count, sign.Samples.Count);
                
                signInRepository.Samples = samples;
            }
            repository.SaveChanges();
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

        public int Recognize(Sample sample)
        {
            return staticSignRecognitionAlgorithm.Recognize(sample);
        }

        [Obsolete("Remover")]
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
    }
}