using Dominio.Sinais;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using Dominio.Gerenciamento;
using Dominio.Sinais.Frames;

namespace Aplicacao.Hubs
{
    public class SinaisEstaticos : Hub
    {
        private readonly GerenciadorSinaisEstaticos gerenciadorSinaisEstaticos;

        public SinaisEstaticos(GerenciadorSinaisEstaticos gerenciadorSinaisEstaticos)
        {
            this.gerenciadorSinaisEstaticos = gerenciadorSinaisEstaticos;
        }

        public bool Reconhecer(int idSinal, Frame[] amostra)
        {
            return gerenciadorSinaisEstaticos.Reconhecer(idSinal, amostra);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, IList<Frame> amostra)
        {
            gerenciadorSinaisEstaticos.SalvarAmostraDoSinal(descricao, conteudoDoArquivoDeExemplo, amostra);
        }
    }
}