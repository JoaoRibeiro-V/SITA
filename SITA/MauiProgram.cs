using Microsoft.Extensions.Logging;
using SITA.src.Model;
using SITA.src.Storage;
using SITA.src.Util;

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
            LoadImportAsync().Wait();
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

    public static async Task LoadImportAsync()
        {
            System.Diagnostics.Debug.WriteLine("Calling load import async");
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("import.json");
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                System.Diagnostics.Debug.WriteLine("JSON loaded: " + json);

                var handler = new JsonHandler();
                handler.LoadFromString(json, AppStorage);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading import.json: {ex.Message}");
            }
        }
    }
}