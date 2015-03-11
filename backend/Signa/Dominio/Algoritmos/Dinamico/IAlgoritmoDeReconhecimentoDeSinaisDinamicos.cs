using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos
    {
        int Reconhecer(IList<Frame> amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisDinamicos dados);
    }
}