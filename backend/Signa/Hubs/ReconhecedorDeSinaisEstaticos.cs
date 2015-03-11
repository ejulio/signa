using Microsoft.AspNet.SignalR;
using Signa.Dados;
using Signa.Dominio.Sinais;

namespace Signa.Hubs
{
    public class ReconhecedorDeSinaisEstaticos : Hub
    {
        private readonly SinaisEstaticosController sinaisEstaticosController;

        public ReconhecedorDeSinaisEstaticos(SinaisEstaticosController sinaisEstaticosController)
        {
            this.sinaisEstaticosController = sinaisEstaticosController;
        }

        public int Reconhecer(Frame[] amostra)
        {
            return sinaisEstaticosController.Reconhecer(amostra);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, Frame amostra)
        {
            sinaisEstaticosController.SalvarAmostraDoSinal(descricao, conteudoDoArquivoDeExemplo, amostra);
        }
    }
}