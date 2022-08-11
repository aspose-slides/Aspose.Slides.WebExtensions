using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class NumberHelper
    {
        public const int DEFAULT_PRECISION = 3;
        public static string ToCssNumber(float number, int precision, bool trimEndZeros)
        {
            return ToCssNumber(((double)number), precision, trimEndZeros);
        }
        public static string ToCssNumber(double number, int precision, bool trimEndZeros)
        {
            string format = "0." + new string('0', precision);
            string result = number.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
            if (trimEndZeros)
            {
                result = result.TrimEnd('0').TrimEnd('.');
            }
            return result;
        }
        public static string ToCssNumber(float number, int precision)
        {
            return ToCssNumber(((double)number), precision, true);
        }
        public static string ToCssNumber(double number, int precision)
        {
            return ToCssNumber(number, precision, true);
        }
        public static string ToCssNumber(float number)
        {
            return ToCssNumber(((double)number), DEFAULT_PRECISION, true);
        }
        public static string ToCssNumber(double number)
        {
            return ToCssNumber(number, DEFAULT_PRECISION, true);
        }
    }
}
