using Signa.Model;
using System;
using System.Linq;

namespace Signa.Data
{
    public class SignSamplesController
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

        public int Count { 
            get 
            {
                return repository.Count;
            } 
        }

        private IRepository<Sign> repository;

        private SignSamplesController()
        {
            repository = new SignRepository(SamplesFilePath);
        }

        public void Load()
        {
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
                foreach (var sample in sign.Samples)
                {
                    var samples = new SignSample[signInRepository.Samples.Count + sign.Samples.Count];
                    Array.Copy(signInRepository.Samples.ToArray(), samples, signInRepository.Samples.Count);
                    Array.Copy(sign.Samples.ToArray(), 0, samples, signInRepository.Samples.Count, sign.Samples.Count);

                    signInRepository.Samples = samples;
                }
            }

        }

        public void Save()
        {
            repository.SaveChanges();
        }

        public Sign GetByIndex(int index)
        {
            return repository.GetByIndex(index);
        }
    }
}