using SITA;
using SITA.src.Model;
using SITA.src.Storage;
using SITA.src.Util;

namespace SITA.Maui;

public partial class SignInPage : ContentPage
{
    private readonly Storage<User> _userStorage;

    public SignInPage()
    {
        InitializeComponent();
        _userStorage = MauiProgram.AppStorage.GetStorage<User>();
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            ShowError("Preencha todos os campos.");
            return;
        }

        User? user = _userStorage.GetData(name);

        if (user == null || !PasswordHandler.VerifyPassword(password, user.Senha))
        {

            ShowError($"Usuário ou senha inválidos. {password}");
            return;
        }

        MauiProgram.AppSession.InitSession(user.AccessType);
        await Shell.Current.GoToAsync("ProfilePage");
    }

    private async void OnGoToSignUpClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SignUpPage");
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }
}