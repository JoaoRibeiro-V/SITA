using SITA.src.Model;
using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class ResponsavelController
    {
        static Storage<Responsavel> ClassStorage = MauiProgram.AppStorage.GetStorage<Responsavel>();
        public static void Register(Responsavel obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
        public static void Delete(Responsavel obj)
        {
            ClassStorage.Remove(obj.Id.ToString());
        }
        public static Responsavel? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            return ClassStorage.GetDataByField(field, value);
        }
        public static Responsavel CreateByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var responsavel = new Responsavel
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

            return responsavel;
        }
        public static void AddParentesco(Responsavel responsavel, Aluno aluno, int tipoParentesco)
        {
            responsavel.AddParentesco(aluno, tipoParentesco);
        }
    }
}
