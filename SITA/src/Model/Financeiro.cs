using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITA.src.Util;

namespace SITA.src.Model
{
    public abstract class Financeiro
    {
        public Guid Id { get; set; }
        public float Valor { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataEmissao { get; set; }
        public string? Observacao { get; set; }
        public User? UsuarioCriacao { get; set; } = null;
        public Financeiro()
        {
            Id = Guid.NewGuid();
            DataEmissao = DateTime.Now;
            UsuarioCriacao = Session.GetLoggedInUser();
        }
    }
}
