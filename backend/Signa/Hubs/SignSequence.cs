using Microsoft.AspNet.SignalR;
using Signa.Data.Repository;
using Signa.Domain.Signs;
using Signa.Domain.Signs.Static;
using System;

namespace Signa.Hubs
{
    public class SignSequence : Hub
    {
        private readonly IRepository<Sign> repository;

        public SignSequence(IRepository<Sign> repository)
        {
            this.repository = repository;
        }

        public SignInfo GetNextSign(int previousSignIndex)
        {
            var random = new Random();
            int index = random.Next(repository.Count);
            var sign = repository.GetByIndex(index);

            var signInfo = new SignInfo
            {
                Id = index,
                Description = sign.Description,
                ExampleFilePath = sign.ExampleFilePath
            };

            return signInfo;
        }
    }
}