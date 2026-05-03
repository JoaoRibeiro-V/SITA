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
<<<<<<< HEAD

=======
        /* Classe para obter uma turma específica do armazenamento
         * Caso o segundo parâmetro seja nulo, retorna a turma com a chave igual ao primeiro parâmetro (Por padrão, o Id da turma).
         * Caso contrário, retorna a turma que tenha o valor do campo especificado igual ao segundo parâmetro.
         * 
         * EXEMPLO:
         * 
         * TurmaController.Get("Nome", "Turma A") -> Retorna a turma que tenha o Nome igual a "Turma A"
         * TurmaController.Get("Id", "123e4567-e89b-12d3-a456-426614174000") -> Retorna a turma que tenha o Id igual a "123e4567-e89b-12d3-a456-426614174000"
         */
>>>>>>> parent of bd99292 (Frontend build | New pages | More backend logic (Routehandler/validator))
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