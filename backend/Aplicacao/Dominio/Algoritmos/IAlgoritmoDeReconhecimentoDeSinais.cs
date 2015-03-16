using System.Collections.Generic;
using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dominio.Algoritmos
{
    public interface IAlgoritmoDeReconhecimentoDeSinais
    {
        int Reconhecer(IList<Frame> amostra);
    }
}