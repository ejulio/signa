using Signa.Model;
using System;
using System.Linq;

namespace Signa.Data
{
    public class SignController
    {
        private IRepository<Sign> repository;

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
    }
}