using Aplicacao.ViewModel;
using Dominio;
using System;
using System.Web.Http;
using Dominio.Gerenciamento;

namespace Aplicacao.Controller
{
    public class SinaisController : ApiController
    {
        private readonly GerenciadorSinaisDinamicos gerenciadorSinaisDinamicos;
        private readonly GerenciadorSinaisEstaticos gerenciadorSinaisEstaticos;
        private readonly InicializadorDeAlgoritmoFacade inicializadorDeAlgoritmo;

        public SinaisController(
            GerenciadorSinaisDinamicos gerenciadorSinaisDinamicos, 
            GerenciadorSinaisEstaticos gerenciadorSinaisEstaticos,
            InicializadorDeAlgoritmoFacade inicializadorDeAlgoritmo)
        {
            this.gerenciadorSinaisDinamicos = gerenciadorSinaisDinamicos;
            this.gerenciadorSinaisEstaticos = gerenciadorSinaisEstaticos;
            this.inicializadorDeAlgoritmo = inicializadorDeAlgoritmo;
        }

        [HttpPost]
        public void SalvarAmostraDeSinalDinamico(SalvarAmostraRequestModel modelo)
        {
            gerenciadorSinaisDinamicos.SalvarAmostraDoSinal(modelo.Descricao, modelo.ConteudoDoArquivoDeExemplo, modelo.Amostra);
        }

        [HttpPost]
        public void SalvarAmostraDeSinalEstatico(SalvarAmostraRequestModel modelo)
        {
            gerenciadorSinaisEstaticos.SalvarAmostraDoSinal(modelo.Descricao, modelo.ConteudoDoArquivoDeExemplo, modelo.Amostra);
        }

        [HttpPost]
        public void TreinarAlgoritmos()
        {
            inicializadorDeAlgoritmo.TreinarAlgoritmosClassificacaoSinais();
        }
    }
    
}