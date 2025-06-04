using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Extensions
{
    public static class StringExtension
    {
        public static string CommaToTitik(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            return text.Replace(',', '.');
        }
    }
}