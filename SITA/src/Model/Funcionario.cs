using System;

namespace SITA.src.Model
{
    public class Funcionario : User
    {
        public string Cargo { get; set; } = string.Empty;
        public decimal SalarioBase { get; set; }
        public DateTime DataAdmissao { get; set; } = DateTime.Now;
        public string Telefone { get; set; } = string.Empty;

        public Funcionario()
        {
            Id = Guid.NewGuid();
        }
    }
}