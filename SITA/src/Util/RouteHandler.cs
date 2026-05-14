using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Util
{
    public static class RouteHandler
    {
        public static List<RouteItem> AllRoutes =>
    MainNav.Concat(CadastroNav).ToList();
        public static bool CanAccess(string path)
        {
            int userLevel = Session.GetInstanceAccess().Level;

            var cleanPath = path.Split('?')[0];
            var route = AllRoutes.FirstOrDefault(r => r.Path == cleanPath);

            return route == null || userLevel >= route.MinAccessLevel;
        }

        public const string Home = "/";
        public const string SignIn = "/signin";
        public const string Aluno = "/alunos";
        public const string Financeiro = "/dashboard-financeiro";
        public const string Turmas = "/turmas";
        public const string Relatorio = "/folha-pagamento";
        public const string Despesas = "/despesas";
        public const string AlunoDetails = "/aluno-details";
        public const string CadastroResponsavel = "/cadastro-responsavel";
        public const string CadastroFuncionario = "/cadastro-funcionario";
        public const string CadastroFinanceiro = "/cadastro-financeiro";
        public const string Perfil = "/profile";

        public class RouteItem
        {
            public string Name { get; set; } = "";
            public string Path { get; set; } = "";
            public int MinAccessLevel { get; set; } = 0;
        }

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

        public static List<RouteItem> MainNav = new()
        {
            new RouteItem { Name = "Aluno", Path = Aluno },
            new RouteItem { Name = "Turma", Path = Turmas, MinAccessLevel = 2 },
            new RouteItem { Name = "Relatórios", Path = Relatorio, MinAccessLevel = 3 },
            new RouteItem { Name = "Receitas", Path = Financeiro, MinAccessLevel = 3 },
            new RouteItem { Name = "Despesas", Path = Despesas, MinAccessLevel = 1 },
        };

        public static List<RouteItem> CadastroNav = new()
        {
            new RouteItem { Name = "Cadastro Responsável", Path = CadastroResponsavel, MinAccessLevel = 3 },
            new RouteItem { Name = "Cadastro Funcionário", Path = CadastroFuncionario, MinAccessLevel = 3 },
            new RouteItem { Name = "Cadastro Financeiro", Path = CadastroFinanceiro, MinAccessLevel = 3 },
        };
    }
}
