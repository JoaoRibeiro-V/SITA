using SITA.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SITA.src.Controller
{
    public static class DespesaController
    {
      
        public static void Register(Despesa obj) => BaseController<Despesa>.Register(obj, f => f.Id.ToString());
        public static void Delete(Despesa obj) => BaseController<Despesa>.Delete(obj, f => f.Id.ToString());
        public static Despesa? Get(string field, string? value) => BaseController<Despesa>.Get(field, value);
        public static List<Despesa> GetAll() => BaseController<Despesa>.GetAll();

      
        public static List<Despesa> GetByResponsavel(string nome)
        {
            return GetAll().Where(d => d.QuemPagou != null &&
                                       d.QuemPagou.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
        }

       
        public static List<Despesa> GetByStatus(Financeiro.FinanceStatus pago)
        {
            return GetAll().Where(d => d.Status == pago).ToList();
        }

        // Busca despesas por Nota Fiscal ou CNPJ do fornecedor.
      
        public static List<Despesa> SearchFiscal(string termo)
        {
            return GetAll().Where(d => (d.NumeroNotaFiscal != null && d.NumeroNotaFiscal.Contains(termo)) ||
                                       (d.CnpjFornecedor != null && d.CnpjFornecedor.Contains(termo))).ToList();
        }
    }
}