using System;
using System.Collections.Generic;
using System.Linq;

namespace SITA.src.Model
{
    public class Responsavel : User
    {
        public static Dictionary<int, string> TipoParentescoMap = new()
        {
            { 0, "Mãe" },
            { 1, "Pai" },
            { 2, "Tio" },
            { 3, "Tia" },
            { 4, "Avô" },
            { 5, "Avó" },
            { 6, "Outro" }
        };
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public List<Parentesco> Parentescos { get; set; } = new List<Parentesco>();

        public Responsavel()
        {
            Id = Guid.NewGuid();
        }

        public void AddParentesco(Aluno aluno, int tipo)
        {
            if (aluno == null) return;
            if (!Parentescos.Any(p => p.AlunoId == aluno.Id))
            {
                Parentescos.Add(new Parentesco
                {
<<<<<<< HEAD
                    AlunoId = aluno.Id,
                    NomeAluno = aluno.Nome,
                    Tipo = tipo
                });
=======
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
                return Responsavel.TipoParentescoMap[Tipo];
>>>>>>> bd992929f2e600202fe3945e1cee5003b33648a3
            }
        }

        public class Parentesco
        {
            public static readonly Dictionary<int, string> ListaTipos = new Dictionary<int, string>
            {
                { 0, "Mãe" },
                { 1, "Pai" }, 
                { 2, "Tio" }, 
                { 3, "Tia" },
                { 4, "Avô" }, 
                { 5, "Avó" }, 
                { 6, "Outro" }
            };

            public Guid AlunoId { get; set; }
            public string? NomeAluno { get; set; }
            public int Tipo { get; set; } = 6;
            public int Prioridade { get; set; } = 0;
            public string GetDescricaoTipo() => ListaTipos.ContainsKey(Tipo) ? ListaTipos[Tipo] : "Outro";
        }
    }
}