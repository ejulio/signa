using Dominio.Sinais;
using System.Collections.Generic;

namespace Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinais
    {
        int Reconhecer(IList<Frame> amostra);
    }
}