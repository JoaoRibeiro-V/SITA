using SITA.src.Model;
using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SITA.src.Controller
{
    public static class EstoqueController
    {
        private static Storage<ItemEstoque> GetStorage() => GeneralStorage.GetStorage<ItemEstoque>();

        public static void Register(ItemEstoque obj) => BaseController<ItemEstoque>.Register(obj, i => i.Id.ToString());
        public static void Delete(ItemEstoque obj) => BaseController<ItemEstoque>.Delete(obj, i => i.Id.ToString());
        public static ItemEstoque? Get(string field, string? value) => BaseController<ItemEstoque>.Get(field, value);
        public static List<ItemEstoque> GetAll() => BaseController<ItemEstoque>.GetAll();

        // Registra entrada de itens no estoque e gera despesa automaticamente
        public static void EntradaEstoque(ItemEstoque item, int quantidade, float precoCusto, string? fornecedor = null)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
            if (precoCusto <= 0) throw new ArgumentException("Preço de custo deve ser maior que zero.");

            item.Quantidade += quantidade;
            item.PrecoCusto = precoCusto;
            if (fornecedor != null) item.Fornecedor = fornecedor;

            // Gera despesa automaticamente pela compra
            var despesa = new Despesa
            {
                Descricao = $"Compra de {quantidade}x {item.Nome}",
                Valor = precoCusto * quantidade,
                Fornecedor = fornecedor ?? item.Fornecedor,
                Categoria = "Estoque",
                DataVencimento = DateTime.Now,
                DataReferente = DateTime.Now,
                DataPagamento = DateTime.Now,
                Observacao = $"Entrada de estoque — item: {item.Nome}, qtd: {quantidade}"
            };
            despesa.Status = Financeiro.FinanceStatus.Pago;
            DespesaController.Register(despesa);
        }

        // Registra venda de item e gera receita automaticamente
        public static void VendaEstoque(ItemEstoque item, int quantidade, Aluno? aluno, Responsavel? responsavel)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
            if (item.Quantidade < quantidade)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {item.Quantidade}");

            item.Quantidade -= quantidade;

            // Gera receita automaticamente pela venda
            var receita = new Receita
            {
                Descricao = $"Venda de {quantidade}x {item.Nome}",
                Valor = item.PrecoVenda * quantidade,
                Aluno = aluno,
                Responsavel = responsavel,
                Origem = "Estoque",
                DataVencimento = DateTime.Now,
                DataReferente = DateTime.Now,
                DataPagamento = DateTime.Now,
            };
            receita.Type = Receita.ReceitaTipo.Aquisicao;
            receita.Status = Financeiro.FinanceStatus.Pago;
            ReceitaController.Register(receita);
        }
    }
}