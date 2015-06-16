using Aplicacao.ViewModel;
using Dominio.Persistencia;
using Dominio.Sinais;
using Microsoft.AspNet.SignalR;

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
            var indice = indiceDoSinalAnterior + 1;
            if (indice == repositorio.Quantidade)
                indice = 0;

            var sinal = repositorio.BuscarPorIndice(indice);
            return new ProximoSinalResponseModel
            {
                Id = sinal.Id,
                IdReconhecimento = sinal.IdNoAlgoritmo,
                Descricao = sinal.Descricao,
                CaminhoParaArquivoDeExemplo = sinal.CaminhoParaArquivoDeExemplo,
                Tipo = sinal.Tipo
            };
        }
    }
}