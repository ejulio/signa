using Dominio.Algoritmos;
using Dominio.Persistencia;
using Dominio.Sinais;

namespace Dominio.Gerenciamento
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