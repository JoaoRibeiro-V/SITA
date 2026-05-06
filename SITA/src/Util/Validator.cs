using SITA.src.Model;
using System;
using System.Linq;
using SITA.src.Storage;
using System.Net.Mail;

namespace SITA.src.Util
{
    public static class Validator
    {
        public static string? ValidateRequired(params (string Value, string Field)[] fields)
        {
            foreach (var (value, field) in fields)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return $"O campo '{field}' é obrigatório.";
            }
            return null;
        }

        public static string? ValidateCPF(string cpf)
        {
            if (cpf.Length != 11)
                return "CPF inválido.";

            return null;
        }

        public static string? ValidateEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return null;
            }
            catch
            {
                return "Email inválido.";
            }
        }

        public static string? ValidatePassword(string senha)
        {
            if (senha.Length < 6)
                return "A senha deve ter pelo menos 6 caracteres.";

            return null;
        }

        public static string? ValidateDuplicateCPF(string cpf)
        {
            var storage = GeneralStorage.GetStorage<User>();

            var existing = storage.GetDataByField("CPF", cpf);

            if (existing != null)
                return "Já existe um usuário com esse CPF.";

            return null;
        }

        public static string? ValidateDuplicateEmail(string email)
        {
            var storage = GeneralStorage.GetStorage<User>();

            var exists = storage.Values
                .Any(u => u.Email == email);

            if (exists)
                return "Já existe um usuário com esse email.";

            return null;
        }
        public static string? ValidateDuplicateMatricula(string matricula)
        {
            var storage = GeneralStorage.GetStorage<Aluno>();
            var exists = storage.Values
                .Any(u => u.RA == matricula);
            if (exists)
                return "Já existe um usuário com essa matrícula.";
            return null;
        }
        public static string? ValidateLogin(string cpf, string senha)
        {
            if (string.IsNullOrWhiteSpace(cpf) || string.IsNullOrWhiteSpace(senha))
                return "Preencha todos os campos.";

            if (cpf.Length != 11)
                return "CPF inválido.";

            return null;
        }
    }
}