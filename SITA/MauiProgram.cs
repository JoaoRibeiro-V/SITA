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
        public static GeneralStorage AppStorage = new GeneralStorage();
        public static Session AppSession = new Session();
        private static void RegisterStorages()
        {
            AppStorage.AddStorage<User>();
            AppStorage.AddStorage<Aluno>();
            AppStorage.AddStorage<Responsavel>();
            AppStorage.AddStorage<Turma>();
            AppStorage.AddStorage<Funcionario>();
            AppStorage.AddStorage<Receita>();
            AppStorage.AddStorage<Despesa>();
        }
        public static MauiApp CreateMauiApp()
        {
            RegisterStorages();
            EnsureJsonCopied().GetAwaiter().GetResult();
            LoadImportSync();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
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
            ResponsavelController.AddParentesco(newResponsavel, filhoDoFulano, 1);

            JsonHandler.PrintClass(newResponsavel);
            System.Diagnostics.Debug.WriteLine($"Responsável: {newResponsavel.Nome}, Aluno: {newResponsavel.GetParentescos()?[0].Aluno.Nome}, Parentesco: {newResponsavel.GetParentescos()?[0].GetParentesco()}");
            return builder.Build();
        }

        public static void LoadImportSync()
        {
            System.Diagnostics.Debug.WriteLine("Calling sync import");

            string path = Path.Combine(FileSystem.AppDataDirectory, "import.json");
            System.Diagnostics.Debug.WriteLine("Looking for file at: " + path);
            System.Diagnostics.Debug.WriteLine("File exists? " + File.Exists(path));

            if (!File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine("File not found: " + path);
                return;
            }

            string json = File.ReadAllText(path);

            System.Diagnostics.Debug.WriteLine("JSON loaded: " + json);

            var handler = new JsonHandler();
            handler.LoadFromString(json, AppStorage);

            System.Diagnostics.Debug.WriteLine("IMPORT FINISHED");
        }
        public static async Task EnsureJsonCopied()
        {
            string targetPath = Path.Combine(FileSystem.AppDataDirectory, "import.json");

            System.Diagnostics.Debug.WriteLine("Copying JSON...");

            using var stream = await FileSystem
                .OpenAppPackageFileAsync("import.json")
                .ConfigureAwait(false);

            using var reader = new StreamReader(stream);
            string content = await reader.ReadToEndAsync().ConfigureAwait(false);

            File.WriteAllText(targetPath, content);

            System.Diagnostics.Debug.WriteLine("Copy complete");
        }
    }
}