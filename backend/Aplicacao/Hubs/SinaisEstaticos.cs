using System.Collections.Generic;
using Aplicacao.Dados;
using Aplicacao.Dominio.Sinais;
using Microsoft.AspNet.SignalR;

namespace Aplicacao.Hubs
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