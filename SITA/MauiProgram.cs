using Microsoft.Extensions.Logging;
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
            AppStorage.AddStorage<Aluno>(); // adicionei pra testar (mas talvez fique assim)
            AppStorage.AddStorage<Responsavel>();
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
            Storage<User> userStorage = AppStorage.GetStorage<User>();
            Storage<Responsavel> responsavelStorage = AppStorage.GetStorage<Responsavel>();
            Storage<Aluno> alunoStorage = AppStorage.GetStorage<Aluno>();

            User? userGet = userStorage.GetDataByField("Email", "admin@gmail.com");
            if (userGet != null)
            {

                string jsonString = JsonSerializer.Serialize(userGet, new JsonSerializerOptions { WriteIndented = true });
                System.Diagnostics.Debug.WriteLine("User found:" + jsonString);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("User not found");
            }

            Responsavel fulano = new Responsavel
            {
                Nome = "Fulano de Tal",
                Email = "fulano@example.com",
                Telefone = "123456789",
            };
            responsavelStorage.AddData(fulano.Id.ToString(), fulano);
            Aluno filhoDoFulano = new Aluno
            {
                Nome = "Ciclano de Tal",
                RA = "12345",
            };
            alunoStorage.AddData(filhoDoFulano.Id.ToString(), filhoDoFulano);
            fulano.AddParentesco(filhoDoFulano, 1); // 1 = pai

            System.Diagnostics.Debug.WriteLine($"Responsável: {fulano.Nome}, Aluno: {fulano.GetParentescos()?[0].Aluno.Nome}, Parentesco: {fulano.GetParentescos()?[0].GetParentesco()}");
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