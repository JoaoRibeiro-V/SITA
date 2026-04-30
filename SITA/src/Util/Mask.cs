using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Util
{
    public static class Mask
    {
        /* Classe Mask:
         * Esta classe fornece métodos estáticos para formatar e remover máscaras de strings,
         * como CPF e números de telefone.
         * 
         * Exemplo de uso:
        ==========================================================================
        MASK DE CPF:
        private void OnCpfInput(ChangeEventArgs e)
        {
            var digits = new string((e.Value?.ToString() ?? "")
              .Where(char.IsDigit)
              .ToArray());

            if (digits.Length > 11)
               digits = digits[..11];

            userName = Mask.FormatCpf(digits);
        }
        ==========================================================================

        ==========================================================================
        REMOVER MASK:
        var userStorage = MauiProgram.AppStorage.GetStorage<User>();
        User? user = userStorage.GetDataByField("CPF", Mask.RemoveMask(userName));
        ==========================================================================
         */
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
