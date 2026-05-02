using Microsoft.Extensions.Logging;
using SITA.src.Controller;
using SITA.src.Model;
using SITA.src.Storage;
using SITA.src.Util;
using System.Text.Json;

namespace SITA
{
    public static class MauiProgram
    {
       
        public static GeneralStorage<object> AppStorage = new GeneralStorage<object>();
        public static Session AppSession = new Session();

        private static void RegisterStorages(MauiAppBuilder builder)
        {
           
            builder.Services.AddSingleton<IStorage<User>, GeneralStorage<User>>();
            builder.Services.AddSingleton<IStorage<Aluno>, GeneralStorage<Aluno>>();
            builder.Services.AddSingleton<IStorage<Responsavel>, GeneralStorage<Responsavel>>();
            builder.Services.AddSingleton<IStorage<Turma>, GeneralStorage<Turma>>();
            builder.Services.AddSingleton<IStorage<Funcionario>, GeneralStorage<Funcionario>>();
            builder.Services.AddSingleton<IStorage<Receita>, GeneralStorage<Receita>>();
            builder.Services.AddSingleton<IStorage<Despesa>, GeneralStorage<Despesa>>();
            builder.Services.AddSingleton<IStorage<Salario>, GeneralStorage<Salario>>();
        }

        public static MauiApp CreateMauiApp()
        {
            // Inicialização dos storages no objeto estático global
            AppStorage.AddStorage<User>();
            AppStorage.AddStorage<Aluno>();
            AppStorage.AddStorage<Responsavel>();
            AppStorage.AddStorage<Turma>();
            AppStorage.AddStorage<Funcionario>();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

            builder.Services.AddMauiBlazorWebView();

            RegisterStorages(builder);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            EnsureJsonCopied().GetAwaiter().GetResult();
            LoadImportSync();

            User newUser = new User
            {
                Nome = "Responsável 1",
                Email = "responsavel@gmail.com",
                CPF = "11111111111"
            };
            newUser.Senha = BCrypt.Net.BCrypt.HashPassword("responsavel123", newUser.Salt);

            UserController.Register(newUser);

            Responsavel newResponsavel = ResponsavelController.CreateByUser(newUser);
            newResponsavel.Telefone = "123456789";
            newResponsavel.Endereco = "Rua Exemplo, 123";

            Aluno filhoDoFulano = new Aluno
            {
                Nome = "Ciclano de Tal",
                RA = "12345",
            };

            AlunoController.Register(filhoDoFulano);

            newResponsavel.AddParentesco(filhoDoFulano, 1);

            JsonHandler.PrintClass(newResponsavel);

            var pTeste = newResponsavel.Parentescos.FirstOrDefault();
            if (pTeste != null)
            {
                System.Diagnostics.Debug.WriteLine($"Responsável: {newResponsavel.Nome}, Aluno: {pTeste.NomeAluno}, Parentesco: {pTeste.GetDescricaoTipo()}");
            }
          

            return builder.Build();
        }

        public static void LoadImportSync()
        {
            System.Diagnostics.Debug.WriteLine("Calling sync import");
            string path = Path.Combine(FileSystem.AppDataDirectory, "import.json");
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            var handler = new JsonHandler();
            handler.LoadFromString(json, (GeneralStorage<object>)AppStorage);
            System.Diagnostics.Debug.WriteLine("IMPORT FINISHED");
        }

        public static async Task EnsureJsonCopied()
        {
            string targetPath = Path.Combine(FileSystem.AppDataDirectory, "import.json");
            using var stream = await FileSystem.OpenAppPackageFileAsync("import.json");
            using var reader = new StreamReader(stream);
            string content = await reader.ReadToEndAsync();
            File.WriteAllText(targetPath, content);
            System.Diagnostics.Debug.WriteLine("Copy complete");
        }
    }
}