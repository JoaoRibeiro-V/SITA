using System;

namespace SITA.src.Model
{
    public class LogEstoque
    {
        public enum TipoOperacao { Entrada, Venda, CadastroItem }
        public Guid Id { get; set; }
        public Guid? FinanceiroId { get; set; }
        public TipoOperacao Tipo { get; set; }
        public DateTime DataHora { get; set; }
        public Guid ItemId { get; set; }
        public string NomeItem { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float ValorTotal { get; set; }
        public string? OperadorNome { get; set; }
        public string? OperadorCpf { get; set; }
        public string? Fornecedor { get; set; } // Entrada
        public string? AlunoNome { get; set; } // Venda
        public string? Observacao { get; set; }
        public LogEstoque()
        {
            Id = Guid.NewGuid();
            DataHora = DateTime.Now;
        }
        public string GetTipoTexto() => Tipo switch
        {
            TipoOperacao.Entrada => "Entrada",
            TipoOperacao.Venda => "Venda",
            TipoOperacao.CadastroItem => "Cadastro",
            _ => "?"
        };
        public string GetTipoBadgeClass() => Tipo switch
        {
            TipoOperacao.Entrada => "bg-success",
            TipoOperacao.Venda => "bg-primary",
            TipoOperacao.CadastroItem => "bg-secondary",
            _ => "bg-dark"
        };
    }
}