using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IAlgoritmoDeReconhecimentoDeSinaisDinamicos
    {
        int Reconhecer(IList<Frame> amostra);
        void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisDinamicos dados);
    }
}