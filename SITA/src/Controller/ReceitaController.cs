using SITA.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class ReceitaController
    {
        public static void Register(Receita obj) => BaseController<Receita>.Register(obj, f => f.Id.ToString());
        public static void Delete(Receita obj) => BaseController<Receita>.Delete(obj, f => f.Id.ToString());
        public static Receita? Get(string field, string? value) => BaseController<Receita>.Get(field, value);
        public static List<Receita> GetAll() => BaseController<Receita>.GetAll();
        public static void GerarTaxaMatricula(Aluno aluno, Responsavel responsavel, decimal valorTaxa)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));
            if (valorTaxa <= 0)
                throw new ArgumentOutOfRangeException(nameof(valorTaxa), "O valor da taxa deve ser maior que zero.");
            Receita Receita = new Receita
            {
                Id = Guid.NewGuid(),
                Aluno = aluno,
                Responsavel = responsavel,
                Valor = (float)valorTaxa,
                DataVencimento = DateTime.Now.AddDays(30),
                Descricao = $"Taxa referente à matrícula do aluno(a) {aluno.Nome}"
            };
            Receita.Type = Receita.ReceitaTipo.Taxas;
            Register(Receita);
        }
        public static void GerarCarneAluno(Aluno aluno, Responsavel responsavel, decimal valorMensalidade, DateTime fimMatricula)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));
            if (valorMensalidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(valorMensalidade), "O valor da mensalidade deve ser maior que zero.");
            if (fimMatricula <= DateTime.Now)
                throw new ArgumentOutOfRangeException(nameof(fimMatricula), "A data de término da matrícula deve ser futura.");
            DateTime dataAtual = DateTime.Now;
            int qtdMeses = ((fimMatricula.Year - dataAtual.Year) * 12) + fimMatricula.Month - dataAtual.Month;
            for (int i = 0; i < qtdMeses; i++)
            {
                DateTime dataVencimento = dataAtual.AddMonths(i);
                if (i == 0)
                {
                    dataVencimento = new DateTime(dataVencimento.Year, dataVencimento.Month, DateTime.DaysInMonth(dataVencimento.Year, dataVencimento.Month));
                }
                Receita Receita = new Receita
                {
                    Id = Guid.NewGuid(),
                    Aluno = aluno,
                    Responsavel = responsavel,
                    Valor = (float)valorMensalidade,
                    DataVencimento = dataVencimento,
                    DataReferente = dataAtual.AddMonths(i),
                    Descricao = $"Mensalidade do aluno(a) {aluno.Nome} referente ao mês de {dataVencimento:MMMM/yyyy}"
                };
                Receita.Type = Receita.ReceitaTipo.Mensalidade;
                
                Register(Receita);
            }
        }
    }
}
