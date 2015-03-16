using System.Collections.Generic;
using Dominio.Sinais;

namespace Aplicacao.ViewModel
{
    public class SalvarAmostraRequestModel
    {
        public string Descricao { get; set; }
        public string ConteudoDoArquivoDeExemplo { get; set; }
        public IList<Frame> Amostra { get; set; }
    }
}