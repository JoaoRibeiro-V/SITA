using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Turno { get; set; } = "Manhã";
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public Funcionario? Professor { get; set; }
        public Turma()
        {
            Id = Guid.NewGuid();
        }
    }
}
