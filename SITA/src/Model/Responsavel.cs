using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    public class Responsavel : User
    {
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        private List<Parentesco> Parentescos { get; set; } = new List<Parentesco>();
        public Responsavel()
        {
            Id = Guid.NewGuid();
            Parentescos = new List<Parentesco>();
        }
        public List<Parentesco>? GetParentescos()
        {
            return Parentescos;
        }
        public void AddParentesco(Aluno aluno, int tipo)
        {
            Parentescos?.Add(new Parentesco { Aluno = aluno, Tipo = tipo });
        }
        public Aluno? GetAlunoByField(string field, string value)
        {
            foreach (var parentesco in Parentescos)
            {
                var aluno = parentesco.Aluno;
                var property = typeof(Aluno).GetProperty(field);
                if (property != null)
                {
                    var propertyValue = property.GetValue(aluno)?.ToString();
                    if (propertyValue == value)
                    {
                        return aluno;
                    }
                }
            }
            return null;
        }
        public class Parentesco
        {
            private Dictionary<int, string> TipoParentesco = new Dictionary<int, string>
            {
                { 0, "Mãe" },
                { 1, "Pai" },
                { 2, "Tio" },
                { 3, "Tia" },
                { 4, "Avô" },
                { 5, "Avó" },
                { 6, "Outro" }
            };
            public int Prioridade { get; set; } // menor, mais prioridade
            public Aluno Aluno { get; set; }
            public int Tipo { get; set; }
            public Parentesco()
            {
                Prioridade = 0;
                Tipo = 6; // outro
            }
            public string GetParentesco()
            {
                return TipoParentesco[Tipo];
            }
        }

    }
}
