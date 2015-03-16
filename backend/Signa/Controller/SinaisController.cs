using System;
using System.Collections.Generic;
using System.Web.Http;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Dominio.Sinais;
using Signa.ViewModel;

namespace Signa.Controller
{
    public class SinaisController : ApiController
    {
        private readonly SinaisDinamicosController sinaisDinamicosController;
        private readonly SinaisEstaticosController sinaisEstaticosController;

        public SinaisController(SinaisDinamicosController sinaisDinamicosController, SinaisEstaticosController sinaisEstaticosController)
        {
            this.sinaisDinamicosController = sinaisDinamicosController;
            this.sinaisEstaticosController = sinaisEstaticosController;
        }

        [HttpPost]
        public void SalvarAmostraDeSinalDinamico(SalvarAmostraRequestModel modelo)
        {
            sinaisDinamicosController.SalvarAmostraDoSinal(modelo.Descricao, modelo.ConteudoDoArquivoDeExemplo, modelo.Amostra);
        }

        [HttpPost]
        public void SalvarAmostraDeSinalEstatico(SalvarAmostraRequestModel modelo)
        {
            sinaisEstaticosController.SalvarAmostraDoSinal(modelo.Descricao, modelo.ConteudoDoArquivoDeExemplo, modelo.Amostra);
        }
    }
    
}