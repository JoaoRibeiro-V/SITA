using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    public class Receita : Financeiro
    {
        public enum ReceitaTipo
        {
            Mensalidade = 0,
            Aquisicao = 1,
            Outros = 2
        }
        public enum ReceitaStatus
        {
            EmAndamento = 0,
            Pago = 1,
            EmAtraso = 2
        }
        public string Origem { get; set; }
        public Aluno Aluno { get; set; }
        public Responsavel Responsavel { get; set; }
        public ReceitaStatus Status { get; set; }
        public ReceitaTipo Type { get; set; }
        public DateTime DataVencimento { get; set; }
        public Receita()
        {
                Id = Guid.NewGuid();
                DataEmissao = DateTime.Now;
                Status = 0; // Em andamento
        }
        public string GetStatusTexto() => Status switch
        {
            ReceitaStatus.EmAndamento => "Em andamento",
            ReceitaStatus.Pago => "Pago",
            ReceitaStatus.EmAtraso => "Em atraso",
            _ => "Indefinido"
        };
        public string GetTypeTexto() => Type switch
        {
            ReceitaTipo.Mensalidade => "Mensalidade",
            ReceitaTipo.Aquisicao => "Aquisição",
            ReceitaTipo.Outros => "Outros",
            _ => "Indefinido"
        };

    }
}
