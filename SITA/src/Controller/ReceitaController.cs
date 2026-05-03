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
                Receita receita = new Receita
                {
                    Id = Guid.NewGuid(),
                    Aluno = aluno,
                    Responsavel = responsavel,
                    Valor = (float)valorMensalidade,
                    DataVencimento = dataVencimento,
                    Descricao = $"Mensalidade do aluno {aluno.Nome} referente ao mês de {dataVencimento:MMMM/yyyy}",
                    Type = 0 // Mensalidade
                };
                Register(receita);
            }
        }
    }
}
