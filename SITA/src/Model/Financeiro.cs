using SITA.src.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SITA.src.Model.Receita;

namespace SITA.src.Model
{
    public abstract class Financeiro
    {
        public enum FinanceStatus
        {
            EmAndamento = 0,
            Pago = 1,
            Pendente = 2,
            EmAtraso = 3
        }
        public FinanceStatus Status
        {
            get
            {
                if (_status == FinanceStatus.Pago)
                    return FinanceStatus.Pago;

                if (DataVencimento < DateTime.Now)
                    return FinanceStatus.EmAtraso;
                if (DataReferente.Month == DateTime.Now.Month)
                    return FinanceStatus.Pendente;

                return FinanceStatus.EmAndamento;
            }
            set
            {
                _status = value;
            }
        }
        private FinanceStatus _status;
        public class Desconto
        {
            public string Descricao = "";
            public float Valor = 0;
        }
        public class Extras
        {
            public string Descricao = "";
            public float Valor = 0;
        }
        public Guid Id { get; set; }
        public float Valor { get; set; }
        public List<Desconto> Descontos { get; set; } = new List<Desconto>();
        public List<Extras> Bonus { get; set; } = new List<Extras>();
        public string? Descricao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataReferente { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public string? Observacao { get; set; }
        public User? UsuarioCriacao { get; set; } = null;
        public Financeiro()
        {
            Id = Guid.NewGuid();
            DataEmissao = DateTime.Now;
            DataReferente = DateTime.Now;
            UsuarioCriacao = Session.GetLoggedInUser();
            _status = FinanceStatus.EmAndamento;
        }
        public FinanceStatus GetStatus()
        {
            return Status;
        }
        public string GetStatusTexto() => Status switch
        {
            FinanceStatus.EmAndamento => "Em andamento",
            FinanceStatus.Pago => "Pago",
            FinanceStatus.EmAtraso => "Em atraso",
            FinanceStatus.Pendente => "Pendente",
            _ => "Desconhecido"
        };
        public bool IsPago()
        {
            return Status == FinanceStatus.Pago;
        }
        public float GetValorTotal()
        {
            float valorFinal = Valor;
            return Valor - Descontos.Sum(d => d.Valor) + Bonus.Sum(b => b.Valor);
        }
    }
}
