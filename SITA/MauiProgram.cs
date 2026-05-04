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
            JsonHandler.ImportFull(json);

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