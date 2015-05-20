using Dominio.Sinais;
using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos
{
    public interface IAlgoritmoClassificacaoSinais
    {
        int Classificar(IList<Frame> amostra);
    }
}