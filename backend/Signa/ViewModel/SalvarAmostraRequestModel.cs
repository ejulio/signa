using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Signa.ViewModel
{
    public class SalvarAmostraRequestModel
    {
        public string Descricao { get; set; }
        public string ConteudoDoArquivoDeExemplo { get; set; }
        public IList<Frame> Amostra { get; set; }
    }
}