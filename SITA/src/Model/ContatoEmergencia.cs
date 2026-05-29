using System;

namespace SITA.src.Model
{
    public class ContatoEmergencia
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string GrauParentesco { get; set; } = string.Empty;

        public ContatoEmergencia()
        {
            Id = Guid.NewGuid();
        }
    }
}