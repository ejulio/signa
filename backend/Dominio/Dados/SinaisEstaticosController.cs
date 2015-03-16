using Aplicacao.Dados.Repositorio;
using Aplicacao.Dominio.Algoritmos;
using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dados
{
    public class SinaisEstaticosController : SinaisController
    {
        public SinaisEstaticosController(IRepositorio<Sinal> repositorio, 
            IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos) 
            : base(repositorio, algoritmoDeReconhecimentoDeSinaisEstaticos)
        {
        }
    }
}