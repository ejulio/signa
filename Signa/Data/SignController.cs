﻿using Signa.Model;
using System;
using System.IO;
using System.Linq;
using Signa.Util;

namespace Signa.Data
{
    public class SignController
    {
        private IRepository<Sign> repository;

        public const string SamplesDirectory = "samples/";

        public SignController(IRepository<Sign> repository)
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

        public Sign GetRandomSign(int lastSignIndex)
        {
            var random = new Random();
            int index;
            do
            {
                index = random.Next(repository.Count);
            }
            while (index == lastSignIndex);

            return repository.GetByIndex(index);
        }

        public void CreateSampleFileIfNotExists(string signDescription, string signSample)
        {
            var file = SamplesDirectory + signDescription.Hyphenate() + ".json";

            if (File.Exists(file))
                return;

            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(signSample);
            }
        }
    }
}