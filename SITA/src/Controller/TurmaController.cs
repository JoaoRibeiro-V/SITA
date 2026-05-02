using SITA.src.Model;
using SITA.src.Storage;

namespace SITA.src.Controller
{
    public static class TurmaController
    {
        static IStorage<Turma> ClassStorage = MauiProgram.AppStorage.GetStorage<Turma>();

        public static void Register(Turma obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }

        public static Turma? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            var storage = (GeneralStorage<Turma>)ClassStorage;
            return storage.GetDataByField(t =>
                field == "Nome" ? t.Nome == value :
                t.Id.ToString() == value);
        }

   
        public static void AddAlunoToTurma(Turma turma, Aluno aluno)
        {
            if (turma == null || aluno == null) return;

            if (!turma.AlunosIds.Contains(aluno.Id))
            {
                turma.AlunosIds.Add(aluno.Id);
            }
        }
    }
}