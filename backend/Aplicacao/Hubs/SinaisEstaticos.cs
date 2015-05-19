using Dominio.Sinais;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using Dominio.Gerenciamento;
using Dominio.Sinais.Frames;

namespace Aplicacao.Hubs
{
    public class SinaisEstaticos : Hub
    {
        private readonly SinaisEstaticosController sinaisEstaticosController;

        public SinaisEstaticos(SinaisEstaticosController sinaisEstaticosController)
        {
            this.sinaisEstaticosController = sinaisEstaticosController;
        }

        public bool Reconhecer(int idSinal, Frame[] amostra)
        {
            return sinaisEstaticosController.Reconhecer(idSinal, amostra);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, IList<Frame> amostra)
        {
            sinaisEstaticosController.SalvarAmostraDoSinal(descricao, conteudoDoArquivoDeExemplo, amostra);
        }
    }
}