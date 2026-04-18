using SITA;
using SITA.src.Model;
using SITA.src.Model;
using SITA.src.Storage;
using System.Xml;

namespace SITA.Maui;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Pega o acesso da sessão ativa e o armazenamento de usuários
        var access = MauiProgram.AppSession.GetInstanceAccess();
        var userStorage = MauiProgram.AppStorage.GetStorage<User>();

        // Acessa o usuário correspondente ao nível de acesso da sessão ativa
        User? current = userStorage.Values
            .FirstOrDefault(u => u.AccessType.Level == access.Level);

        if (current == null)
        {
            Shell.Current.GoToAsync("//SignInPage");
            return;
        }

        NameLabel.Text = $"Nome: {current.Name}";
        CpfLabel.Text = $"CPF: {current.CPF}";
        IdadeLabel.Text = $"Idade: {current.Idade}";
        NascimentoLabel.Text = $"Nascimento: {current.dataNascimento}";
        AccessLabel.Text = $"Nível de acesso: {access.GetAccessName()}";
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Logout: Reseta a sessão e redireciona para a página de login
        MauiProgram.AppSession.InitSession(new AccessType { Level = -1 });
        await Shell.Current.GoToAsync("//SignInPage");
    }
}