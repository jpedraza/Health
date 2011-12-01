using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support
{
    internal static class ExtensionMethods
    {
        internal static string f(this string str, params string[] args)
        {
            return string.Format(str, args);
        }
    }
}
