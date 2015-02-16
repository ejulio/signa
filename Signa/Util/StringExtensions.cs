using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Util
{
    public static class StringExtensions
    {
        public static string Hyphenate(this string value)
        {
            return value.Replace(' ', '-');
        }
    }
}