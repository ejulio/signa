using Microsoft.AspNet.SignalR;
using Signa.Dados;
using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Hubs
{
    public class SinaisEstaticos : Hub
    {
        private readonly SinaisEstaticosController sinaisEstaticosController;

        public SinaisEstaticos(SinaisEstaticosController sinaisEstaticosController)
        {
            this.sinaisEstaticosController = sinaisEstaticosController;
        }

        public int Reconhecer(Frame[] amostra)
        {
            return sinaisEstaticosController.Reconhecer(amostra);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, IList<Frame> amostra)
        {
            sinaisEstaticosController.SalvarAmostraDoSinal(descricao, conteudoDoArquivoDeExemplo, amostra);
        }
    }
}