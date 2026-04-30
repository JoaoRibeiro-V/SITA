using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataUltimoAcesso { get; set; }
        public bool Ativo { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string Salt { get; set; }
        public AccessType AccessType { get; set; } = new AccessType();
        public User()
        {
            Id = Guid.NewGuid();
            Ativo = true;
            DataCriacao = DateTime.Now;
            DataUltimoAcesso = DateTime.Now;
            AccessType.Level = 0;
            Salt = BCrypt.Net.BCrypt.GenerateSalt();
        }
    }
}
