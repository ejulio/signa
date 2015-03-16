
namespace Aplicacao.Util
{
    public static class StringExtensions
    {
        public static string Underscore(this string texto)
        {
            return texto.Replace(' ', '_');
        }

        public static string RemoverAcentos(this string texto)
        {
            var textoCodificado = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            return System.Text.Encoding.UTF8.GetString(textoCodificado);
        }
    }
}