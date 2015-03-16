using Dominio.Dados;
using Dominio.Sinais;
using Microsoft.AspNet.SignalR;

namespace Aplicacao.Hubs
{
    public class SinaisDinamicos : Hub
    {
        private readonly SinaisDinamicosController sinaisDinamicosController;

        public SinaisDinamicos(SinaisDinamicosController sinaisDinamicosController)
        {
            this.sinaisDinamicosController = sinaisDinamicosController;
        }

        public int Reconhecer(Frame[] amostra)
        {
            return sinaisDinamicosController.Reconhecer(amostra);
        }

        public int ReconhecerPrimeiroFrame(Frame[] amostra)
        {
            return sinaisDinamicosController.ReconhecerPrimeiroFrame(amostra);
        }

        public int ReconhecerUltimoFrame(Frame[] amostra)
        {
            return sinaisDinamicosController.ReconhecerUltimoFrame(amostra);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, Frame[] amostra)
        {
            sinaisDinamicosController.SalvarAmostraDoSinal(descricao, conteudoDoArquivoDeExemplo, amostra);
        }
    }
}