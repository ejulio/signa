using Dominio.Dados;
using Dominio.Sinais;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

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