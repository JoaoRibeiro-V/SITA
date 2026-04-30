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
        public string Nome { get; set; }
        public Turma()
        {
            Id = Guid.NewGuid();
        }
    }
}
