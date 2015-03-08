using Microsoft.AspNet.SignalR;
using Signa.Dados.Repositorio;
using Signa.Domain.Sinais;
using Signa.Domain.Sinais.Estatico;
using System;

namespace Signa.Hubs
{
    public class SignSequence : Hub
    {
        private readonly IRepositorio<SinalEstatico> repositorio;

        public SignSequence(IRepositorio<SinalEstatico> repositorio)
        {
            this.repositorio = repositorio;
        }

        public InformacoesDoSinal GetNextSign(int previousSignIndex)
        {
            var random = new Random();
            int index = random.Next(repositorio.Quantidade);
            var sign = repositorio.BuscarPorIndice(index);

            var signInfo = new InformacoesDoSinal
            {
                Id = index,
                Description = sign.Description,
                ExampleFilePath = sign.ExampleFilePath
            };

            return signInfo;
        }
    }
}