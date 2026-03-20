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
            Aluno exemplo2 = new Aluno();
            exemplo2.Name = "Ciclano";

            Storage<Aluno> alunosStorage = storage.GetStorage<Aluno>();
            alunosStorage.AddData(exemplo1.Name, exemplo1);

            foreach (var aluno in alunosStorage.storages) {
                Console.WriteLine(aluno.Key);
            }


        }

        public class Aluno
        {
            public string? Name { get; set; }
        }
    }
}
