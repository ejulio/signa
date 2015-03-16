using System.Web.Http;
using Aplicacao.ViewModel;
using Dominio.Dados;

namespace Aplicacao.Controller
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