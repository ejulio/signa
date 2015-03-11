using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(IList<Frame> frame);
        void Treinar(IGeradorDeDadosDeSinaisEstaticos dados);
    }
}