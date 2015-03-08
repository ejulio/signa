using Microsoft.AspNet.SignalR;
using Signa.Dados.Repositorio;
using System;
using Signa.Dominio.Sinais;

namespace Signa.Hubs
{
    public class SequenciaDeSinais : Hub
    {
        private readonly IRepositorio<Sinal> repositorio;

        public SequenciaDeSinais(IRepositorio<Sinal> repositorio)
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
                Descricao = sign.Descricao,
                CaminhoParaArquivoDeExemplo = sign.CaminhoParaArquivoDeExemplo
            };

            return signInfo;
        }
    }
}