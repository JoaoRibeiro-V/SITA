using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    public class Turma
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public Funcionario? Professor { get; set; }
        public Turma()
        {
            Id = Guid.NewGuid();
        }
    }
}
