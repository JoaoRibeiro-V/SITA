using SITA.src.Model;
using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SITA.src.Controller
{
    public static class FuncionarioController
    {
        // Método seguro para pegar o storage sem crashar na inicialização
        private static Storage<Funcionario> GetStorage() => GeneralStorage.GetStorage<Funcionario>();

        public static void Register(Funcionario obj) => BaseController<Funcionario>.Register(obj, f => f.Id.ToString());

        public static void Delete(Funcionario obj) => BaseController<Funcionario>.Delete(obj, f => f.Id.ToString());

        public static Funcionario? Get(string field, string? value) => BaseController<Funcionario>.Get(field, value);

        public static List<Funcionario> GetAll() => BaseController<Funcionario>.GetAll();

      
        public static Funcionario CreateByUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

           
            return new Funcionario
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
                AccessType = user.AccessType,
                
            };
        }

        // Método extra para o Dashboard: Total da Folha de Pagamento
        public static decimal GetTotalFolhaSalarial()
        {
            return GetAll().Where(f => f.Ativo).Sum(f => f.SalarioBase);
        }
    }
}