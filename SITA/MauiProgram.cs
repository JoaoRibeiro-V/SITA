using Microsoft.Extensions.Logging;
using SITA.src.Model;
using SITA.src.Storage;
using SITA.src.Util;

namespace SITA.Maui;

public static class MauiProgram
{
    public static GeneralStorage AppStorage = new GeneralStorage();
    public static Session AppSession = new Session();
    private static void RegisterStorages()
    {
        AppStorage.AddStorage<User>();
    }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        RegisterStorages();
        

        builder.Services.AddTransient<SignInPage>();
        builder.Services.AddTransient<SignUpPage>();
        builder.Services.AddTransient<ProfilePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    // Chamado após app ser criado, mas antes de mostrar a primeira página
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
        catch(Exception ex) {
            System.Diagnostics.Debug.WriteLine($"Error loading import.json: {ex.Message}");
        }
    }
}