namespace SITA.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rotas de navegação
        Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
        Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
    }
}