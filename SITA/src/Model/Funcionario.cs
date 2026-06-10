    using System;

    namespace SITA.src.Model
    {
        // A mágica acontece aqui: ":" significa herança
        public class Funcionario : User
        {
            // Propriedades de RH que o User comum não tem
            public string? Cargo { get; set; }
            public string? Departamento { get; set; }
            public DateTime DataAdmissao { get; set; }
            public decimal SalarioBase { get; set; }
            public string? Telefone { get; set; }
            public string? ChavePix { get; set; }

            public Funcionario() : base() // Chama o construtor do User para gerar o ID
            {
                DataAdmissao = DateTime.Today;
                Ativo = true;
            }
        }
    }