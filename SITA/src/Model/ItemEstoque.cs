using System;

namespace SITA.src.Model
{
    // Representa um item físico no estoque escolar (uniforme, material, etc)
    public class ItemEstoque
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Categoria { get; set; }  // "Uniforme", "Material", "Outros"
        public int Quantidade { get; set; }
        public float PrecoCusto { get; set; }   // preço de compra (gera despesa)
        public float PrecoVenda { get; set; }   // preço de venda (gera receita)
        public string? Fornecedor { get; set; }
        public DateTime DataCadastro { get; set; }

        public ItemEstoque()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        // Margem de lucro em percentual
        public float GetMargem()
        {
            if (PrecoCusto == 0) return 0;
            return ((PrecoVenda - PrecoCusto) / PrecoCusto) * 100f;
        }

        // Valor total em estoque pelo custo
        public float GetValorTotalCusto() => Quantidade * PrecoCusto;

        // Valor total em estoque pelo preço de venda
        public float GetValorTotalVenda() => Quantidade * PrecoVenda;
    }
}