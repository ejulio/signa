using Microsoft.AspNet.SignalR;
using Signa.Domain.Signs;
using Signa.Domain.Signs.Static;
using System;
using Signa.Dados.Repositorio;

namespace Signa.Hubs
{
    public class SignSequence : Hub
    {
        private readonly IRepositorio<SinalEstatico> repositorio;

        public SignSequence(IRepositorio<SinalEstatico> repositorio)
        {
            this.repositorio = repositorio;
        }

        public SignInfo GetNextSign(int previousSignIndex)
        {
            var random = new Random();
            int index = random.Next(repositorio.Quantidade);
            var sign = repositorio.BuscarPorIndice(index);

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