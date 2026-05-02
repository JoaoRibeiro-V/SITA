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
            var session = MauiProgram.AppSession;
            int userLevel = session.GetInstanceAccess().Level;

            var cleanPath = path.Split('?')[0];
            var route = AllRoutes.FirstOrDefault(r => r.Path == cleanPath);

            return route == null || userLevel >= route.MinAccessLevel;
        }

        public const string Home = "/";
        public const string SignIn = "/signin";
        public const string Aluno = "/alunos";
        public const string Financeiro = "/financeiro";
        public const string Turmas = "/turmas";
        public const string Relatorio = "/relatorio";
        public const string Pedidos = "/pedidos";
        public const string AlunoDetails = "/aluno-details";
        public const string CadastroAluno = "/cadastro-aluno";
        public const string CadastroResponsavel = "/cadastro-responsavel";
        public const string CadastroTurma = "/cadastro-turma";
        public const string CadastroFuncionario = "/cadastro-funcionario";
        public const string CadastroFinanceiro = "/cadastro-financeiro";
        public const string Perfil = "/profile";

        public class RouteItem
        {
            public string Name { get; set; } = "";
            public string Path { get; set; } = "";
            public int MinAccessLevel { get; set; } = 0;
        }

        public static List<RouteItem> MainNav = new()
        {
            new RouteItem { Name = "Aluno", Path = Aluno },
            new RouteItem { Name = "Financeiro", Path = Financeiro },
            new RouteItem { Name = "Turma", Path = Turmas },
            new RouteItem { Name = "Relatórios", Path = Relatorio },
            new RouteItem { Name = "Pedidos", Path = Pedidos },
            new RouteItem { Name = "Perfil", Path = Perfil },
        };

        public static List<RouteItem> CadastroNav = new()
        {
            new RouteItem { Name = "Cadastro Aluno", Path = CadastroAluno },
            new RouteItem { Name = "Cadastro Responsável", Path = CadastroResponsavel },
            new RouteItem { Name = "Cadastro Turma", Path = CadastroTurma },
            new RouteItem { Name = "Cadastro Funcionário", Path = CadastroFuncionario },
            new RouteItem { Name = "Cadastro Financeiro", Path = CadastroFinanceiro, MinAccessLevel = 4 },
        };
    }
}
