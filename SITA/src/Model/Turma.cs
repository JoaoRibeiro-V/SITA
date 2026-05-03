namespace SITA.src.Model
{
    public class Turma
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public List<Guid> AlunosIds { get; set; } = new List<Guid>();

        public Guid? ProfessorId { get; set; }

        public Turma() => Id = Guid.NewGuid();
    }
}