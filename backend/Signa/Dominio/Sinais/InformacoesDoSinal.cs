
namespace Signa.Dominio.Sinais
{
    public class InformacoesDoSinal
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string CaminhoParaArquivoDeExemplo { get; set; }
        public TipoSinal Tipo { get; set; }
    }
}