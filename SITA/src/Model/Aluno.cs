using System;
using System.Collections.Generic;

namespace SITA.src.Model
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string? RA { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Religiao { get; set; }
        public string? NomePediatra { get; set; }
        public string? OrientacoesEmergencia { get; set; }
        public List<string> Alergias { get; set; } = new List<string>();
        public List<string> CondicoesEspeciais { get; set; } = new List<string>();
        public List<string> HistoricoFinanceiroIds { get; set; } = new List<string>();
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Guid? TurmaId { get; set; }
        public string? ResponsavelCPF { get; set; }

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
    }
}