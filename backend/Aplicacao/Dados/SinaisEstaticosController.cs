using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;
using Signa.Dominio.Sinais;

namespace Signa.Dados
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