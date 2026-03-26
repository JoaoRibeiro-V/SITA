using System;
using System.Linq;
using System.Text.Json;

namespace StorageTest
{
    internal class Program
    {
        public static GeneralStorage? storage;
        private static void registerStorages()
        {
            storage = new GeneralStorage();
            storage.AddStorage<Aluno>();
            storage.AddStorage<Diretor>();
        }
        static void Main(string[] args)
        {
            registerStorages();

            Aluno exemplo1 = new Aluno();
            exemplo1.Name = "Fulano";
            exemplo1.Idade = 5;
            Aluno exemplo2 = new Aluno();
            exemplo2.Name = "Ciclano";
            exemplo2.Idade = 15;
            Diretor diretor1 = new Diretor();
            diretor1.Name = "Diretor 1";
            diretor1.Idade = 30;
            diretor1.cpf = "53111";

            Storage<Aluno> alunosStorage = storage.GetStorage<Aluno>();
            Storage<Diretor> diretorStorage = storage.GetStorage<Diretor>();

            alunosStorage.AddData(exemplo1.Name, exemplo1);
            alunosStorage.AddData(exemplo2.Name, exemplo2);
            diretorStorage.AddData(diretor1.cpf, diretor1);

            Aluno get = alunosStorage.GetData("Fulano");
            Console.WriteLine(get.Idade);
            get.Idade = 6;
            Console.WriteLine(alunosStorage.GetData("Fulano").Idade);

            Console.WriteLine($"Qt alunos: {alunosStorage.Count}");
            Console.WriteLine($"Qt diretores: {diretorStorage.Count}");


        }

        public class Aluno : User{
            public string? RA {  get; set; }
            public string? nomeResponsavel { get; set; }
        }
        public class Diretor : User{
            public string? cpf { get; set; }
        }
        public class User{
            public string? Name { get; set; }
            public int? Idade { get; set; }
        }
    }
}