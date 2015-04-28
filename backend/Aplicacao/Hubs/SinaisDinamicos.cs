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

        public bool Reconhecer(int idSinal, Frame[] amostra)
        {
            return sinaisDinamicosController.Reconhecer(idSinal, amostra);
        }

        public bool ReconhecerPrimeiroFrame(int idSinal, Frame[] amostra)
        {
            return sinaisDinamicosController.ReconhecerPrimeiroFrame(idSinal, amostra);
        }

        public bool ReconhecerUltimoFrame(int idSinal, Frame[] amostraPrimeiroFrame, Frame[] amostraUltimoFrame)
        {
            return sinaisDinamicosController.ReconhecerUltimoFrame(idSinal, amostraPrimeiroFrame, amostraUltimoFrame);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, Frame[] amostra)
        {
            sinaisDinamicosController.SalvarAmostraDoSinal(descricao, conteudoDoArquivoDeExemplo, amostra);
        }
    }
}