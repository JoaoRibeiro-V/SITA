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
        }
        public static MauiApp CreateMauiApp()
        {
            RegisterStorages();
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

    public static async Task LoadImportAsync()
        {
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