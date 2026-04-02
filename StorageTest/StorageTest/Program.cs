using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StorageTest
{
    internal class Program
    {
        public static GeneralStorage? storage;
        private static string importPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\alunos.json";
        private static void registerFromJson()
        {

        }
        private static void registerStorages()
        {
            storage = new GeneralStorage();
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

            //Pegando os Storages de Aluno e User
            Storage<Aluno> alunosStorage = storage.GetStorage<Aluno>();
            Storage<User> userStorage = storage.GetStorage<User>();

            //Imprimindo na tela quantos alunos existem cadastrados na memória

            Console.WriteLine($"Qt alunos: {alunosStorage.Count}");

        }

         
        
        public class Aluno : User{
            public string? RA {  get; set; }
            public string[]? nomeResponsavel { get; set; }
            public string[]? telResponsavel { get; set; }
            public string[]? condEspeciais { get; set; }
            public string[]? alergias { get; set; }
            public string? religiao { get; set; }
            public string? nomePediatra { get; set; }
        }
       
        public class User{
            public string? Name { get; set; }
            public int? Idade { get; set; }
            public string? CPF { get; set; }           
            public string? dataNascimento { get; set; }
            public AccessType? AccessType { get; set; }
        }
        public class AccessType
        {
            public int? Level { get; set; }
            public string? Name { get; set; }
        }
    }
}
