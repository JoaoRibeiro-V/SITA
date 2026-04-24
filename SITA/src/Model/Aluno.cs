using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace SITA.src.Model
{
    public class Aluno
    {
   
        public string? Name { get; set; }
        public string? CPF { get; set; } 
        public string? DataNascimento { get; set; }
        public string? TurmaId { get; set; } // Atrela o aluno à sala
        public string? Alergias { get; set; }
        public string? Religiao { get; set; }
        public string? CondicoesEspeciais { get; set; }
        public string? NomePediatra { get; set; }
        public string? OrientacoesEmergencia { get; set; }

        public List<string> HistoricoFinanceiroIds { get; set; } = new List<string>();

        public Aluno()
        {
            // Inicializa a lista para evitar erro de null
        }
    }
}