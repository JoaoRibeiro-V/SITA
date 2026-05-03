using System;

namespace SITA.src.Model
{
    public class Despesa : Financeiro
    {
        public string? Fornecedor { get; set; }
        public string? Categoria { get; set; }
        public DateTime DataPagamento { get; set; }
        public int Status { get; set; } // 0: Pendente, 1: Pago

        public Despesa() : base()
        {
            Status = 0;
        }

        public bool IsPago => Status == 1;
    }
}