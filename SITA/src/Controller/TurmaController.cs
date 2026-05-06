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
        /* Classe estática para controle de turmas
         * 
         * Esta classe fornece métodos estáticos para registrar, obter e gerenciar turmas.
         * Utiliza a classe Storage para persistência de dados.
         */
        static Storage<Turma> ClassStorage = GeneralStorage.GetStorage<Turma>();
        // Registra uma nova turma no armazenamento
        public static void Register(Turma obj) => BaseController<Turma>.Register(obj, t => t.Id.ToString());
        public static void Delete(Turma obj) => BaseController<Turma>.Delete(obj, t => t.Id.ToString());
        /* Classe para obter uma turma específica do armazenamento
         * Caso o segundo parâmetro seja nulo, retorna a turma com a chave igual ao primeiro parâmetro (Por padrão, o Id da turma).
         * Caso contrário, retorna a turma que tenha o valor do campo especificado igual ao segundo parâmetro.
         * 
         * EXEMPLO:
         * 
         * TurmaController.Get("Nome", "Turma A") -> Retorna a turma que tenha o Nome igual a "Turma A"
         * TurmaController.Get("Id", "123e4567-e89b-12d3-a456-426614174000") -> Retorna a turma que tenha o Id igual a "123e4567-e89b-12d3-a456-426614174000"
         */
        public static Turma? Get(string field, string? value) => BaseController<Turma>.Get(field, value);
        public static List<Turma> GetAll() => BaseController<Turma>.GetAll();
        // Método para adicionar um aluno a uma turma
        public static void AddAlunoToTurma(Turma turma, Aluno aluno)
        {
            if (turma == null)
                throw new ArgumentNullException(nameof(turma));
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));
            turma.Alunos.Add(aluno);
            aluno.Turma = turma;
        }
        public static void MoverAlunoParaOutraTurma(Turma newTurma, Turma oldTurma, Aluno aluno)
        {
            if (newTurma == null)
                throw new ArgumentNullException(nameof(newTurma));
            if (oldTurma == null)
                throw new ArgumentNullException(nameof(oldTurma));
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));
            if (newTurma == oldTurma) return;
            newTurma.Alunos.Add(aluno);
            oldTurma.Alunos.Remove(aluno);
            aluno.Turma = newTurma;
        }
    }
}
