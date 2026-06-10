using SITA.src.Model;
using SITA.src.Storage;
using SITA.src.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SITA.src.Controller
{
    public static class EstoqueController
    {
        public static void Register(ItemEstoque obj)
        {
            BaseController<ItemEstoque>.Register(obj, i => i.Id.ToString());

            // Log de cadastro
            var operador = Session.GetLoggedInUser();
            LogEstoqueController.Register(new LogEstoque
            {
                Tipo = LogEstoque.TipoOperacao.CadastroItem,
                ItemId = obj.Id,
                NomeItem = obj.Nome,
                Quantidade = 0,
                ValorUnitario = (float)Math.Round((double)obj.PrecoCusto, 2),
                ValorTotal = 0,
                Fornecedor = obj.Fornecedor,
                OperadorNome = operador?.Nome,
                OperadorCpf = operador?.CPF,
                Observacao = $"Item cadastrado — custo: R$ {obj.PrecoCusto:N2}, venda: R$ {obj.PrecoVenda:N2}"
            });
        }

        public static void Delete(ItemEstoque obj) =>
            BaseController<ItemEstoque>.Delete(obj, i => i.Id.ToString());

        public static ItemEstoque? Get(string field, string? value) =>
            BaseController<ItemEstoque>.Get(field, value);

        public static List<ItemEstoque> GetAll() =>
            BaseController<ItemEstoque>.GetAll();

        public static void EntradaEstoque(ItemEstoque item, int quantidade, float precoCusto, string? fornecedor = null,
            string? quemPagou = null, string? cnpjFornecedor = null, string? numeroNotaFiscal = null, string? chaveAcessoNF = null)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
            if (precoCusto <= 0) throw new ArgumentException("Preço de custo deve ser maior que zero.");

            item.Quantidade += quantidade;
            item.PrecoCusto = (float)Math.Round((double)precoCusto, 2);
            if (fornecedor != null) item.Fornecedor = fornecedor;

            var despesa = new Despesa
            {
                Descricao = $"Compra de {quantidade}x {item.Nome}",
                Valor = precoCusto * quantidade,
                Fornecedor = fornecedor ?? item.Fornecedor,
                Categoria = "Estoque",
                DataVencimento = DateTime.Now.AddDays(7),
                DataReferente = DateTime.Now,
                DataPagamento = DateTime.Now,
                Observacao = $"Entrada de estoque — item: {item.Nome}, qtd: {quantidade}",
                QuemPagou = quemPagou,
                CnpjFornecedor = cnpjFornecedor,
                NumeroNotaFiscal = numeroNotaFiscal,
                ChaveAcessoNF = chaveAcessoNF
            };
            despesa.Status = Financeiro.FinanceStatus.Pendente;
            DespesaController.Register(despesa);

            var operador = Session.GetLoggedInUser();
            LogEstoqueController.Register(new LogEstoque
            {
                Tipo = LogEstoque.TipoOperacao.Entrada,
                ItemId = item.Id,
                NomeItem = item.Nome,
                Quantidade = quantidade,
                ValorUnitario = precoCusto,
                ValorTotal = precoCusto * quantidade,
                Fornecedor = fornecedor ?? item.Fornecedor,
                OperadorNome = operador?.Nome,
                OperadorCpf = operador?.CPF,
                FinanceiroId = despesa.Id,
                Observacao = $"Entrada registrada — despesa gerada automaticamente"
            });
        }

        public static void VendaEstoque(ItemEstoque item, int quantidade, Aluno? aluno)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
            if (item.Quantidade < quantidade)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {item.Quantidade}");

            item.Quantidade -= quantidade;

            var receita = new Receita
            {
                Descricao = $"Venda de {quantidade}x {item.Nome}",
                Valor = item.PrecoVenda * quantidade,
                Aluno = aluno,
                Origem = "Estoque",
                DataVencimento = DateTime.Now.AddDays(7),
                DataReferente = DateTime.Now,
                DataPagamento = DateTime.Now,
            };
            receita.Type = Receita.ReceitaTipo.Aquisicao;
            receita.Status = Financeiro.FinanceStatus.Pendente;
            ReceitaController.Register(receita);

            var operador = Session.GetLoggedInUser();
            LogEstoqueController.Register(new LogEstoque
            {
                Tipo = LogEstoque.TipoOperacao.Venda,
                ItemId = item.Id,
                FinanceiroId = receita.Id,
                NomeItem = item.Nome,
                Quantidade = quantidade,
                ValorUnitario = item.PrecoVenda,
                ValorTotal = item.PrecoVenda * quantidade,
                AlunoNome = aluno?.Nome,
                OperadorNome = operador?.Nome,
                OperadorCpf = operador?.CPF,
                Observacao = $"Venda registrada — receita gerada automaticamente"
            });
        }
    }
}