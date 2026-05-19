using System;

namespace SITA.src.Model
{
    public class Despesa : Financeiro


    {
        public string? Fornecedor { get; set; }
        public string? Categoria { get; set; }

        // Novos Campos Robustos
        public string? QuemPagou { get; set; } // Nome do funcionário
        public string? CnpjEscola { get; set; } = "00.000.000/0001-00"; // CNPJ Fixo da Instituição
        public string? CnpjFornecedor { get; set; }
        public string? NumeroNotaFiscal { get; set; }
        public string? ChaveAcessoNF { get; set; }
        public string? AnexoCaminho { get; set; } // Caminho do PDF/Imagem

        public Despesa() : base()
        {
            DataVencimento = DateTime.Now;
        }
    }
}