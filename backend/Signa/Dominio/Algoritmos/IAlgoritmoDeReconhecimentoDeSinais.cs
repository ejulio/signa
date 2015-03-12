using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinais
    {
        int Reconhecer(IList<Frame> amostra);
    }
}