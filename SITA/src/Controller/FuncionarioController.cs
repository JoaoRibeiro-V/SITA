using SITA.src.Model;
using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class FuncionarioController
    {
        static Storage<Funcionario> ClassStorage = MauiProgram.AppStorage.GetStorage<Funcionario>();
        public static void Register(Funcionario obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
        public static Funcionario? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            return ClassStorage.GetDataByField(field, value);
        }
        public static Funcionario CreateByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var funcionario = new Funcionario
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                CPF = user.CPF,
                Senha = user.Senha,
                Salt = user.Salt,
                DataCriacao = user.DataCriacao,
                DataUltimoAcesso = user.DataUltimoAcesso,
                Ativo = user.Ativo,
                AccessType = user.AccessType
            };

            return funcionario;
        }
    }
}
