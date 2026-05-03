using SITA.src.Model;
using SITA.src.Storage;

namespace SITA.src.Controller
{
    public static class FuncionarioController
    {
        static IStorage<Funcionario> ClassStorage = MauiProgram.AppStorage.GetStorage<Funcionario>();

        public static void Register(Funcionario obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
<<<<<<< HEAD

=======
>>>>>>> parent of bd99292 (Frontend build | New pages | More backend logic (Routehandler/validator))
        public static Funcionario? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            var storage = (GeneralStorage<Funcionario>)ClassStorage;
            return storage.GetDataByField(f =>
                field == "CPF" ? f.CPF == value :
                field == "Email" ? f.Email == value :
                f.Id.ToString() == value);
        }

        public static Funcionario CreateByUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new Funcionario
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
               
                Senha = user.Senha,
                Salt = user.Salt,
                DataCriacao = user.DataCriacao,
                DataUltimoAcesso = user.DataUltimoAcesso,
                Ativo = user.Ativo,
                AccessType = user.AccessType
            };
        }
    }
}