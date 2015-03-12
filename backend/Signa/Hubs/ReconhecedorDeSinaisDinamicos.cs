using Microsoft.AspNet.SignalR;
using Signa.Dados;
using Signa.Dominio.Sinais;

namespace Signa.Hubs
{
    public class ReconhecedorDeSinaisDinamicos : Hub
    {
        private readonly SinaisDinamicosController sinaisDinamicosController;

        public ReconhecedorDeSinaisDinamicos(SinaisDinamicosController sinaisDinamicosController)
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