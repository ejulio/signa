using System.Collections.Generic;
using Newtonsoft.Json;

namespace Signa.Dominio.Sinais
{
    public class Sinal
    {
        public string Descricao { get; set; }
        public string CaminhoParaArquivoDeExemplo { get; set; }
        private IList<Amostra> amostras;

        public IList<Amostra> Amostras
        {
            get { return amostras; }
            set
            {
                amostras = value;
                if (amostras != null && amostras.Count > 0)
                {
                    Tipo = amostras[0].QuantidadeDeFrames > 1 ? TipoSinal.Dinamico : TipoSinal.Estatico;
                }
            }
        }

        public TipoSinal Tipo { get; set; }

        public Sinal()
        {
            Tipo = TipoSinal.Estatico;
            Amostras = new List<Amostra>();
        }
    }
}