using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos : IAlgoritmoDeReconhecimentoDeSinais
    {
        void Treinar(IGeradorDeDadosDeSinaisEstaticos dados);
    }
}