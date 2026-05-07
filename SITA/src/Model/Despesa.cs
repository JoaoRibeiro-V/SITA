using System;
using System.Security.Cryptography.X509Certificates;

namespace SITA.src.Model{
    public class Despesa : Financeiro {

        public string? Fornecedor { get; set; }
        public string? Categoria { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Status { get; set; } // true: Pago, false: Pendente

        public Despesa() : base()
        {
            Status = false; // Começa pendente

        }
        public bool IsPago => Status == true;
    }

}
