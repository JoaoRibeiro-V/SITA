using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    public class Receita : Financeiro
    {
        private Dictionary<int, string> StatusList = new Dictionary<int, string>
        {
            { 0, "Em andamento" },
            { 1, "Pago" },
            { 2, "Em atraso" }
        };
        public string Origem { get; set; }
        public Aluno Aluno { get; set; }
        public Responsavel Responsavel { get; set; }
        public int Status { get; set; }
        public DateTime DataVencimento { get; set; }
        public Receita()
        {
                Id = Guid.NewGuid();
                DataEmissao = DateTime.Now;
                Status = 0; // Em andamento
        }

    }
}
