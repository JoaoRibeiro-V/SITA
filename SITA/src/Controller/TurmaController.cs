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
        static Storage<Turma> ClassStorage = MauiProgram.AppStorage.GetStorage<Turma>();
        // Registra uma nova turma no armazenamento
        public static void Register(Turma obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
        public static void Delete(Turma obj)
        {
            ClassStorage.Remove(obj.Id.ToString());
        }
        /* Classe para obter uma turma específica do armazenamento
         * Caso o segundo parâmetro seja nulo, retorna a turma com a chave igual ao primeiro parâmetro (Por padrão, o Id da turma).
         * Caso contrário, retorna a turma que tenha o valor do campo especificado igual ao segundo parâmetro.
         * 
         * EXEMPLO:
         * 
         * TurmaController.Get("Nome", "Turma A") -> Retorna a turma que tenha o Nome igual a "Turma A"
         * TurmaController.Get("Id", "123e4567-e89b-12d3-a456-426614174000") -> Retorna a turma que tenha o Id igual a "123e4567-e89b-12d3-a456-426614174000"
         */
        public static Turma? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            return ClassStorage.GetDataByField(field, value);
        }
        // Método para adicionar um aluno a uma turma
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
