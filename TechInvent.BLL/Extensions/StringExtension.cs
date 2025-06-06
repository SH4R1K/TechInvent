using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.BLL.Extensions
{
    public static class StringExtension
    {
        public static string? ReturnNullIfEmpty(this string _string)
        {
            if (String.IsNullOrWhiteSpace(_string))
                return null;
            return _string;
        }
        public static bool IsEmpty(this string _string)
        {
            return String.IsNullOrWhiteSpace(_string);
        }
    }
}
