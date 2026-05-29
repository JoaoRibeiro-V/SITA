using System;
using System.Collections.Generic;
using System.Globalization;
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
        var userStorage = GeneralStorage.GetStorage<User>();
        User? user = userStorage.GetDataByField("CPF", Mask.RemoveMask(userName));
        ==========================================================================
         */
        // Source - https://stackoverflow.com/a/1374644
        // Posted by Cogwheel, modified by community. See post 'Timeline' for change history
        // Retrieved 2026-05-15, License - CC BY-SA 4.0

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

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
        public static string FormatCEP(string digits)
        {
            if (string.IsNullOrEmpty(digits))
                return "";

            if (digits.Length <= 5)
                return digits;

            return $"{digits[..5]}-{digits[5..]}";
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

        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
    }
}
