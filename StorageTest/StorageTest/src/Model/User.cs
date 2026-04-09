using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTest.src.Model
{
    internal class User
    {
        public string? Name { get; set; }
        public int? Idade { get; set; }
        public string? CPF { get; set; }
        public string? dataNascimento { get; set; }
        public AccessType AccessType { get; set; } = new AccessType();
        public User()
        {
            AccessType.Level = 0;
            AccessType.Name = "Usuario";
        }
    }
}
