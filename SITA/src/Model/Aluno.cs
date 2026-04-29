using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Collections;

namespace SITA.src.Model
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string? RA { get; set; }
        public int Idade { get; set; }
        public string? Nome { get; set; }
        public string? DataNascimento { get; set; }
        public string? TurmaId { get; set; } // Atrela o aluno à sala
        public string? Religiao { get; set; }
        public string? NomePediatra { get; set; }
        public string? OrientacoesEmergencia { get; set; }
        public List<string>? Alergias { get; set; }
        public List<string>? CondicoesEspeciais { get; set; }
        public List<string> HistoricoFinanceiroIds { get; set; } = new List<string>();
        public DateTime DataCadastro { get; set; }

        public Aluno()
        {
            Id = Guid.NewGuid();
        }
    }
}