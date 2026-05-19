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
            Taxas = 2,
            Outros = 9
        }
        public enum ReceitaStatus
        {
            EmAndamento = 0,
            Pago = 1,
            Pendente = 2,
            EmAtraso = 3
        }
        public string Origem { get; set; }
        public Aluno? Aluno { get; set; } = null;
        public Responsavel? Responsavel { get; set; } = null;
        public ReceitaTipo Type { get; set; }
        public Receita()
        {
                Id = Guid.NewGuid();
                DataEmissao = DateTime.Now;
        }
        public string GetTypeTexto() => Type switch
        {
            ReceitaTipo.Mensalidade => "Mensalidade",
            ReceitaTipo.Aquisicao => "Aquisição",
            ReceitaTipo.Taxas => "Taxas",
            ReceitaTipo.Outros => "Outros",
            _ => "Indefinido"
        };

    }
}
