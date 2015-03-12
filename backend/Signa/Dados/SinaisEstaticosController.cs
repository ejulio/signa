using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using Signa.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Signa.Dominio.Algoritmos;

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