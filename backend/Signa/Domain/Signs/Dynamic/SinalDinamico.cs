using System;
using System.Collections.Generic;

namespace Signa.Domain.Signs.Dynamic
{
    public class SinalDinamico
    {
        public string Descricao { get; set; }
        public string CaminhoParaArquivoDeExemplo { get; set; }
        public IList<AmostraDeSinal> Amostras { get; set; }

        public SinalDinamico()
        {
            Amostras = new List<AmostraDeSinal>();
        }
    }
}