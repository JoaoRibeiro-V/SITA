namespace SITA;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new SITA.Maui.AppShell();

        Task.Run(async () => await SITA.Maui.MauiProgram.LoadImportAsync());
    }
}