using StorageTest.src.Model;
using StorageTest.src.Storage;
using StorageTest.src.Util;
using System.Diagnostics;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StorageTest
{
    internal class Program
    {
        public static GeneralStorage ProgramStorage = new GeneralStorage(); // Classe de armazenamento na memória
        public static Session ProgramSession = new Session(); // Classe de armazenamento de sessão

        // Local do arquivo JSON para dados prontos de antemão
        // Busca a partir da pasta principal do projeto
        private static string importPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\import.json";

        /*
         * Registra os tipos de dados que terão armazenamento em memória.
         * 
         * IMPORTANTE:
         * Todo tipo que será carregado via JSON precisa ser registrado aqui.
        */
        private static void RegisterStorages()
        {
            // Cada tipo cria um Storage<T> separado
            ProgramStorage.AddStorage<Aluno>();
            ProgramStorage.AddStorage<User>();
        }
        static void Main(string[] args)
        {
            //Registrando ProgramStorages na memória
            RegisterStorages();

            //Importando dados do JSON
            JsonHandler importsHandler = new JsonHandler();
            importsHandler.LoadIntoGeneralStorage(importPath, ProgramStorage);

            // Acessa ProgramStorages específicos (Dicionaries)
            Storage<Aluno> alunosStorage = ProgramStorage.GetStorage<Aluno>();
            Storage<User> userStorage = ProgramStorage.GetStorage<User>();

            // Tenta acessar um storage não armazenado
            Storage<Session> sessionStorage = ProgramStorage.TryGetOrAddStorage<Session>();

            // Exemplo de uso:
            // Imprimindo na tela quantos alunos existem cadastrados na memória

            Console.WriteLine($"Qt alunos: {alunosStorage.Count}");

            /*
             * ===========================
             * Teste de sessão do programa
             * ===========================
             */

            // Imprime na tela a atual situação de Sessão do programa
            Console.WriteLine("\n=============================\n");
            Console.WriteLine($"Usuário Logado: {(ProgramSession.GetInstanceAccess().Level < 0 ? "Não" : "Sim")}");
            Console.WriteLine("\n=============================\n");

            // Iniciado a sessão caso usuário for encontrado
            User? newUser = userStorage.GetData("User1");
            if(newUser != null)
            {
                ProgramSession.InitSession(newUser.AccessType);
            }

            // Imprime na tela se o usuário foi encontrado e foi iniciada a sessão
            Console.WriteLine("\n=============================\n");
            Console.WriteLine($"Usuário Logado: {(ProgramSession.GetInstanceAccess().Level >= 0 ? "Sim" : "Não")}");
            Console.WriteLine("\n=============================\n");

        }



        public class Aluno : User
        {
            public string? RA { get; set; }
            public string[]? nomeResponsavel { get; set; }
            public string[]? telResponsavel { get; set; }
            public string[]? condEspeciais { get; set; }
            public string[]? alergias { get; set; }
            public string? religiao { get; set; }
            public string? nomePediatra { get; set; }
        }
    }
}
