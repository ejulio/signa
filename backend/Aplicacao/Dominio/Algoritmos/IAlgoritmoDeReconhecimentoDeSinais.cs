using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinais
    {
        int Reconhecer(IList<Frame> amostra);
    }
}