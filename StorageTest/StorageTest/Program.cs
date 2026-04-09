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
        public static GeneralStorage? storage; // Classe de armazenamento na memória
        public static Session ProgramSession = new Session();

        // Local do arquivo JSON para dados prontos de antemão
        // Busca a partir da pasta principal do projeto
        private static string importPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\import.json";

        /*
         * Registra os tipos de dados que terão armazenamento em memória.
         * 
         * IMPORTANTE:
         * Todo tipo que será carregado via JSON precisa ser registrado aqui.
        */
        private static void registerStorages()
        {
            storage = new GeneralStorage();

            // Cada tipo cria um Storage<T> separado
            storage.AddStorage<Aluno>();
            storage.AddStorage<User>();
        }
        static void Main(string[] args)
        {
            //Registrando storages na memória
            registerStorages();

            //Importando dados do JSON
            JsonHandler importsHandler = new JsonHandler();
            importsHandler.LoadIntoGeneralStorage(importPath, storage);

            // Acessa storages específicos
            Storage<Aluno> alunosStorage = storage.GetStorage<Aluno>();
            Storage<User> userStorage = storage.GetStorage<User>();

            // Exemplo de uso:
            // Imprimindo na tela quantos alunos existem cadastrados na memória

            Console.WriteLine($"Qt alunos: {alunosStorage.Count}");




            Console.WriteLine("\n=============================\n");
            Console.WriteLine($"Usuário Logado: {(ProgramSession.GetInstanceAccess().Level < 0 ? "Não" : "Sim")}");
            Console.WriteLine("\n=============================\n");

            User newUser = new User();
            newUser.AccessType.Level = 0; // Aluno
            //newUser.AccessType.Name = "Aluno(a)";

            ProgramSession.InitSession(newUser.AccessType);

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
