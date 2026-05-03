using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    public class Salario : Financeiro
    {
        public Funcionario Funcionario { get; set; }
        public string MesReferencia { get; set; }
        public bool Pago { get; set; }
        public Salario()
        {
            Id = Guid.NewGuid();
            DataEmissao = DateTime.Now;
            Pago = false;
        }
    }
}
