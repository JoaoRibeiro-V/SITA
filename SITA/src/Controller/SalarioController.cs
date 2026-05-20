using SITA.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class SalarioController
    {
        public static void Register(Salario obj) => BaseController<Salario>.Register(obj, f => f.Id.ToString());
        public static void Delete(Salario obj) => BaseController<Salario>.Delete(obj, f => f.Id.ToString());
        public static Salario? Get(string field, string? value) => BaseController<Salario>.Get(field, value);
        public static List<Salario> GetAll() => BaseController<Salario>.GetAll();
        public static void GerarSalarioFuncionario(Funcionario funcionario, decimal salario)
        {
            
            DateTime dataAtual = DateTime.Now;
            DateTime fimSalario = new DateTime(dataAtual.Year, 12, 30);
            int qtdMeses = ((fimSalario.Year - dataAtual.Year) * 12) + fimSalario.Month - dataAtual.Month;
            for (int i = 0; i < qtdMeses; i++)
            {
                DateTime dataVencimento = dataAtual.AddMonths(i);
                if (i == 0)
                {
                    dataVencimento = new DateTime(dataVencimento.Year, dataVencimento.Month, 30);
                }
                Salario SalarioFuncionario = new Salario
                {
                    Funcionario = funcionario,
                    Valor = (float)salario,
                    DataReferente = new DateTime(DateTime.Now.Year, dataAtual.Month + i, 30),
                    DataVencimento = new DateTime(DateTime.Now.Year, dataAtual.Month + i, 30), // Exemplo: vencimento no dia 30 do mês,
                    Status = Financeiro.FinanceStatus.Pendente,
                    Categoria = "Salário"
                };
                SalarioFuncionario.Descricao = $"Salário de {funcionario.Nome} referente ao mês {SalarioFuncionario.DataReferente:MMMM/yyyy}";
                DespesaController.Register(SalarioFuncionario);
            }
        }
    }
}
