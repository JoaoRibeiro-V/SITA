using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Util
{
    public static class Mask
    {
        public static string FormatCpf(string digits)
        {
            if (digits.Length <= 3)
                return digits;

            if (digits.Length <= 6)
                return $"{digits[..3]}.{digits[3..]}";

            if (digits.Length <= 9)
                return $"{digits[..3]}.{digits[3..6]}.{digits[6..]}";

            return $"{digits[..3]}.{digits[3..6]}.{digits[6..9]}-{digits[9..]}";
        }
        public static string FormatPhone(string digits)
        {
            if (string.IsNullOrEmpty(digits))
                return "";
            if (digits.Length <= 2)
                return $"({digits}";
            if (digits.Length <= 6)
                return $"({digits[..2]}) {digits[2..]}";
            if (digits.Length == 10)
                return $"({digits[..2]}) {digits[2..6]}-{digits[6..]}";
            return $"({digits[..2]}) {digits[2..7]}-{digits[7..]}";
        }
        public static string RemoveMask(string masked)
        {
            return masked.Replace(".", "").Replace("-", "").Replace("(","").Replace(")","");
        }
    }
}
