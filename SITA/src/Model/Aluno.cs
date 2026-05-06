using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Collections;
using SITA.src.Controller;

namespace SITA.src.Model
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string? RA { get; set; }
        public int? Idade { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascimento { get; set; } = DateTime.Today;
        public string? Religiao { get; set; }
        public string? NomePediatra { get; set; }
        public string? OrientacoesEmergencia { get; set; }
        public List<string>? Alergias { get; set; }
        public List<string>? CondicoesEspeciais { get; set; }
        public List<string> HistoricoFinanceiroIds { get; set; } = new List<string>();
        public DateTime DataCadastro { get; set; }
        public Turma? Turma { get; set; }
        public List<Responsavel> Responsaveis { get; set; } = new List<Responsavel>();

        public Aluno()
        {
            Id = Guid.NewGuid();
        }
        public int CalcularIdade()
        {
            if (!DataNascimento.HasValue) return 0;
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Value.Year;
            if (DataNascimento.Value.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
        public void GerarRA()
        {
            RA = "201" + (AlunoController.GetAll().Count + 1).ToString("D4");
        }
    }
}