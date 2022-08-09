using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class CssNumbersExtension
    {
        public const int DEFAULT_PRECISION = 3;
        public static string ToCssNumber(this float number, int precision = DEFAULT_PRECISION, bool trimEndZeros = true)
        {
            return ((double)number).ToCssNumber(precision, trimEndZeros);
        }
        public static string ToCssNumber(this double number, int precision = DEFAULT_PRECISION, bool trimEndZeros = true)
        {
            string format = "0." + new string('0', precision);
            string result = number.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
            if (trimEndZeros)
            {
                result = result.TrimEnd('0').TrimEnd('.');
            }
            return result;
        }
    }
}
