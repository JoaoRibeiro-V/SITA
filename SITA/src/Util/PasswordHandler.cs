using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace SITA.src.Util
{
    /*
     * Classe estática para validação e criptografia em hash de senhas
     * Lida com ações relacionadas a  senha de um usuário
     * Utiliza BCrypt
     */
    public static class PasswordHandler
    {
        // Retorna um hash de senha a partir da string passada
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        // Verifica se o hash do primeiro parâmetro bate com o segundo
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        // Verifica se a string de senha cumpre o mínimo de segurança
        public static bool IsValidPassword(string password)
        {
            /* Casos onde a senha não é válida
             * 
             * 1. Se for nula
             * 2. Se a qt de caracteres é menor ou igual a 6
             * 3. Se não há caracteres em caixa alta
             * 4. Se não há números
             */
            if (password == null) return false;
            if (password.Length <= 6) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsDigit)) return false;

            // Senha válida
            return true;
        }
    }
}
