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
        [HttpPost]
        public void SalvarAmostraDeSinalDinamico(SalvarAmostraRequestModel modelo)
        {
            var controller = new SinaisDinamicosController(
                new RepositorioFactory(Dados.SinaisController.CaminhoDoArquivoDoRepositorio).CriarECarregarRepositorioDeSinaisDinamicos(), 
                null, 
                null,
                null);

            controller.SalvarAmostraDoSinal(modelo.Descricao, modelo.ConteudoDoArquivoDeExemplo, modelo.Amostra);
        }

        [HttpPost]
        public void SalvarAmostraDeSinalEstatico(SalvarAmostraRequestModel modelo)
        {
            var controller = new SinaisEstaticosController(
                new RepositorioFactory(Dados.SinaisController.CaminhoDoArquivoDoRepositorio).CriarECarregarRepositorioDeSinaisEstaticos(),
                null);

            controller.SalvarAmostraDoSinal(modelo.Descricao, modelo.ConteudoDoArquivoDeExemplo, modelo.Amostra);
        }
    }
    
}