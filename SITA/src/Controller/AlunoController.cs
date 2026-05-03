using SITA.src.Model;
using SITA.src.Storage;

namespace SITA.src.Controller
{
    public static class AlunoController
    {
        
        static IStorage<Aluno> ClassStorage = MauiProgram.AppStorage.GetStorage<Aluno>();

        public static void Register(Aluno obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }

        public static Aluno? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }

           
            var storage = (GeneralStorage<Aluno>)ClassStorage;
            return storage.GetDataByField(a =>
                field == "RA" ? a.RA == value :
                field == "Nome" ? a.Nome == value :
                a.Id.ToString() == value);
        }
    }
}