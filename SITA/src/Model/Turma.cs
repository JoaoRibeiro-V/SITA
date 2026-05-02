namespace SITA.src.Model
{
    public class Turma
    {
        public static List<string> Turnos { get; set; } = new List<string>
        {
            "Manhã",
            "Tarde",
            "Noite"
        };
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
<<<<<<< HEAD

        public List<Guid> AlunosIds { get; set; } = new List<Guid>();

        public Guid? ProfessorId { get; set; }

        public Turma() => Id = Guid.NewGuid();
=======
        public string Turno { get; set; } = "Manhã";
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public Funcionario? Professor { get; set; }
        public Turma()
        {
            Id = Guid.NewGuid();
        }
>>>>>>> bd992929f2e600202fe3945e1cee5003b33648a3
    }
}