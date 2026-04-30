using SITA.src.Model;
using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class TurmaController
    {
        static Storage<Turma> ClassStorage = MauiProgram.AppStorage.GetStorage<Turma>();
        public static void Register(Turma obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
        public static Turma? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            return ClassStorage.GetDataByField(field, value);
        }
        public static void AddAlunoToTurma(Turma turma, Aluno aluno)
        {
            if (turma == null)
                throw new ArgumentNullException(nameof(turma));
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));
            turma.Alunos.Add(aluno);
        }
    }
}
