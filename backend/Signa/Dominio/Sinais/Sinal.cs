using System.Collections.Generic;

namespace Signa.Dominio.Sinais
{
    public class Sinal
    {
        public string Descricao { get; set; }
        public string CaminhoParaArquivoDeExemplo { get; set; }
        public IList<Amostra> Amostras { get; set; }

        public Sinal()
        {
            Amostras = new List<Amostra>();
        }
    }
}