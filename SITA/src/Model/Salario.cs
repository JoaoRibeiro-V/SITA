using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    public class Salario : Despesa
    {
        public Funcionario Funcionario { get; set; }
        public Salario()
        {
            Id = Guid.NewGuid();
            DataEmissao = DateTime.Now;
        }
    }
}
