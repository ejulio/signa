using Dominio.Algoritmos;
using Dominio.Dados.Repositorio;
using Dominio.Sinais;

namespace Dominio.Dados
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