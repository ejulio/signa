
using Dominio.Sinais;

namespace Aplicacao.ViewModel
{
    public class ProximoSinalResponseModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string CaminhoParaArquivoDeExemplo { get; set; }
        public TipoSinal Tipo { get; set; }
    }
}