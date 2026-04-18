using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Model
{
    /* Níveis de Acesso de cada usuário do sistema.
     * 
     * Níveis atuais:
     * -1: Não Logado
     * 0 : Responsável
     * 1 : Funcionário
     * 2 : Professor
     * 3 : Secretário
     * 4 : Diretor
     * 
     */
    public class AccessType
    {
        private Dictionary<int, string> NomeNiveis = new Dictionary<int, string>
        {
            { -1, "Não Logado" },
            { 0, "Responsável" },
            { 1, "Funcionário" },
            { 2, "Professor" },
            { 3, "Secretário" },
            { 4, "Diretor" }
        };
        public int Level { get; set; } = -1;
        public string GetAccessName()
        {
            return NomeNiveis[Level];
        }
    }
}
