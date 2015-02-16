
namespace Signa.Util
{
    public static class StringExtensions
    {
        public static string Underscore(this string value)
        {
            return value.Replace(' ', '_');
        }
    }
}