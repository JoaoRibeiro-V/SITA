using SITA.src.Model;
using SITA.src.Storage;

namespace SITA.src.Controller
{
    public static class ResponsavelController
    {
        static IStorage<Responsavel> ClassStorage = MauiProgram.AppStorage.GetStorage<Responsavel>();

        public static void Register(Responsavel obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
<<<<<<< HEAD

=======
>>>>>>> parent of bd99292 (Frontend build | New pages | More backend logic (Routehandler/validator))
        public static Responsavel? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            var storage = (GeneralStorage<Responsavel>)ClassStorage;
            return storage.GetDataByField(r =>
                field == "CPF" ? r.CPF == value :
                field == "Email" ? r.Email == value :
                r.Id.ToString() == value);
        }

        public static Responsavel CreateByUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new Responsavel
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                Senha = user.Senha,
                Salt = user.Salt,
                DataCriacao = user.DataCriacao,
                Ativo = user.Ativo,
                AccessType = user.AccessType
            };
        }

        public static void AddParentesco(Responsavel responsavel, Aluno aluno, int tipoParentesco)
        {
            responsavel.AddParentesco(aluno, tipoParentesco);
        }
    }
}