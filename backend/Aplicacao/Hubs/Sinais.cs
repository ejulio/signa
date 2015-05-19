using Aplicacao.ViewModel;
using Dominio.Sinais;
using Microsoft.AspNet.SignalR;
using System;
using Dominio.Persistencia;

namespace Aplicacao.Hubs
{
    public class Sinais : Hub
    {
        private readonly IRepositorio<Sinal> repositorio;

        public Sinais(IRepositorio<Sinal> repositorio)
        {
            this.repositorio = repositorio;
        }

        public ProximoSinalResponseModel ProximoSinal(int indiceDoSinalAnterior)
        {
            var random = new Random();
            int indice = random.Next(repositorio.Quantidade);
            var sinal = repositorio.BuscarPorIndice(indice);
            
            var informadoesDoSinal = new ProximoSinalResponseModel
            {
                Id = sinal.IndiceNoAlgoritmo,
                Descricao = sinal.Descricao,
                CaminhoParaArquivoDeExemplo = sinal.CaminhoParaArquivoDeExemplo,
                Tipo = sinal.Tipo
            };

            return informadoesDoSinal;
        }
    }
}