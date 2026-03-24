using System;
using System.Linq;
using System.Text.Json;

namespace StorageTest
{
    internal class Program
    {
        public static GeneralStorage storage;
        private static void registerStorages()
        {
            storage = new GeneralStorage();
            storage.AddStorage<Aluno>();
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
            
            Storage<Aluno> alunosStorage = storage.GetStorage<Aluno>();
            alunosStorage.AddData(exemplo1.Name, exemplo1);
            alunosStorage.AddData(exemplo2.Name, exemplo2);

            Console.WriteLine(alunosStorage.Count);


        }

        public class Aluno
        {
            public string? Name { get; set; }
            public int? Idade { get; set; }
        }
    }
}