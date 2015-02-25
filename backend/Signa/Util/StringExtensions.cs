
namespace Signa.Util
{
    public static class StringExtensions
    {
        public static string Underscore(this string value)
        {
            return value.Replace(' ', '_');
        }

        public static string RemoveAccents(this string value)
        {
            var encodedText = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(value);
            return System.Text.Encoding.UTF8.GetString(encodedText);
        }
    }
}