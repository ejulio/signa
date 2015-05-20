using Dominio.Gerenciamento;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using Microsoft.AspNet.SignalR;

namespace Aplicacao.Hubs
{
    public class SinaisDinamicos : Hub
    {
        private readonly GerenciadorSinaisDinamicos gerenciadorSinaisDinamicos;

        public SinaisDinamicos(GerenciadorSinaisDinamicos gerenciadorSinaisDinamicos)
        {
            this.gerenciadorSinaisDinamicos = gerenciadorSinaisDinamicos;
        }

        public bool Reconhecer(int idSinal, Frame[] amostra)
        {
            return gerenciadorSinaisDinamicos.Reconhecer(idSinal, amostra);
        }

        public bool ReconhecerPrimeiroFrame(int idSinal, Frame[] amostra)
        {
            return gerenciadorSinaisDinamicos.ReconhecerPrimeiroFrame(idSinal, amostra);
        }

        public bool ReconhecerUltimoFrame(int idSinal, Frame[] amostraPrimeiroFrame, Frame[] amostraUltimoFrame)
        {
            return gerenciadorSinaisDinamicos.ReconhecerUltimoFrame(idSinal, amostraPrimeiroFrame, amostraUltimoFrame);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, Frame[] amostra)
        {
            gerenciadorSinaisDinamicos.SalvarAmostraDoSinal(descricao, conteudoDoArquivoDeExemplo, amostra);
        }
    }
}