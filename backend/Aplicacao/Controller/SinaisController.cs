using Aplicacao.ViewModel;
using Dominio;
using Dominio.Dados;
using System;
using System.Web.Http;

namespace Aplicacao.Controller
{
    public class SinaisController : ApiController
    {
        private readonly SinaisDinamicosController sinaisDinamicosController;
        private readonly SinaisEstaticosController sinaisEstaticosController;
        private readonly InicializadorDeAlgoritmoFacade inicializadorDeAlgoritmo;

        public SinaisController(
            SinaisDinamicosController sinaisDinamicosController, 
            SinaisEstaticosController sinaisEstaticosController,
            InicializadorDeAlgoritmoFacade inicializadorDeAlgoritmo)
        {
            this.sinaisDinamicosController = sinaisDinamicosController;
            this.sinaisEstaticosController = sinaisEstaticosController;
            this.inicializadorDeAlgoritmo = inicializadorDeAlgoritmo;
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

        [HttpPost]
        public void TreinarAlgoritmos()
        {
            inicializadorDeAlgoritmo.TreinarAlgoritmosDeReconhecimentoDeSinais();
        }
    }
    
}