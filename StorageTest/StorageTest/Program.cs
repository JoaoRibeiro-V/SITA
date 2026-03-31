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
        private static void registerStorages()
        {
            storage = new GeneralStorage();
            storage.AddStorage<Aluno>();
            storage.AddStorage<User>();
        }
        static void Main(string[] args)
        {
            registerStorages();

            Aluno exemplo1 = new Aluno();
            exemplo1.Name = "Fulano";
            exemplo1.Idade = 5;
            exemplo1.RA = 1201212;
            Aluno exemplo2 = new Aluno();
            exemplo2.Name = "Ciclano";
            exemplo2.Idade = 15;
            Diretor diretor1 = new Diretor();
            diretor1.Name = "Diretor 1";
            diretor1.Idade = 30;
            diretor1.cpf = "53111";

            Storage<Aluno> alunosStorage = storage.GetStorage<Aluno>();
            Storage<User> userStorage = storage.GetStorage<User>();
            {
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\alunos.json";
                Console.WriteLine(path);
                string myJson =  File.ReadAllText(path);
                byte[] byteArray = Encoding.UTF8.GetBytes(myJson);

                using (MemoryStream stream = new MemoryStream(byteArray)) { 
                    Aluno newAluno = JsonSerializer.Deserialize<Aluno>(stream);
                    alunosStorage.AddData(newAluno.Name, newAluno);
                    Console.WriteLine($"Adding data from JSON: {JsonSerializer.Serialize(newAluno)}");
                }
            }
            
            alunosStorage.AddData(exemplo1.Name, exemplo1);
            alunosStorage.AddData(exemplo2.Name, exemplo2);
            diretorStorage.AddData(diretor1.cpf, diretor1);
            
            alunosStorage.AddData(exemplo1.RA, exemplo1);
            alunosStorage.AddData(exemplo2.Name, exemplo2);
            diretorStorage.AddData(diretor1.cpf, diretor1);

            Aluno get = alunosStorage.GetData("Fulano");

            
            // VALIDAR RA //     
            alunosStorage.TryGetValue(exemplo1.RA, out get); 

            Console.WriteLine(get.Idade);
            get.Idade = 6;
            Console.WriteLine(alunosStorage.GetData("Fulano").Idade);
            
            Console.WriteLine($"Qt alunos: {alunosStorage.Count}");
            Console.WriteLine($"Qt diretores: {diretorStorage.Count}");

        }

        public class Aluno : User{
            public string? RA {  get; set; }
            
            public string? nomeResponsavel { get; set; }
            public bool ValidarRA()
            {
                return this.RA != null;
            }
        }
        public class Aluno : User{
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
            public string? cpf { get; set; }           
            public string? dataNascimento { get; set; }
            public int? userType { get; set; }
        }
    }
}
