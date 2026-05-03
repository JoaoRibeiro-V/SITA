namespace SITA.src.Model
{
    public class Turma
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
<<<<<<< HEAD

        public List<Guid> AlunosIds { get; set; } = new List<Guid>();

        public Guid? ProfessorId { get; set; }

        public Turma() => Id = Guid.NewGuid();
=======
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public Funcionario? Professor { get; set; }
        public Turma()
        {
            Id = Guid.NewGuid();
        }
>>>>>>> parent of bd99292 (Frontend build | New pages | More backend logic (Routehandler/validator))
    }
}