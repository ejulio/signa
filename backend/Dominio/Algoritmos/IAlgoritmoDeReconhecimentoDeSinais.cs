using Dominio.Sinais;
using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinais
    {
        int Reconhecer(IList<Frame> amostra);
    }
}