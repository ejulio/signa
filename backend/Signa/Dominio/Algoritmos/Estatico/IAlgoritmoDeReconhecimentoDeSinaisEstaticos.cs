using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        int Reconhecer(IList<Frame> frame);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dados);
    }
}