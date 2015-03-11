using System.Collections.Generic;

namespace Signa.Dominio.Sinais
{
    public class Sinal
    {
        public string Descricao { get; set; }
        public string CaminhoParaArquivoDeExemplo { get; set; }

        public TipoSinal Tipo { get; set; }
        
        private IList<IList<Frame>> amostras;

        public IList<IList<Frame>> Amostras
        {
            get { return amostras; }
            set
            {
                amostras = value;
                DefinirTipoDoSinal();
            }
        }

        public Sinal()
        {
            Tipo = TipoSinal.Estatico;
            Amostras = new List<IList<Frame>>();
        }

        private void DefinirTipoDoSinal()
        {
            if (amostras != null && amostras.Count > 0)
                Tipo = EhUmSinalDinamico() ? TipoSinal.Dinamico : TipoSinal.Estatico;
        }

        private bool EhUmSinalDinamico()
        {
            return amostras[0].Count > 1;
        }
    }
}