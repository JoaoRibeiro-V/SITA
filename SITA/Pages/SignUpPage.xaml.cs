using SITA;
using SITA.src.Model;
using SITA.src.Storage;
using SITA.src.Util;
using SITA.src.Model;
using SITA.src.Storage;
using SITA.src.Util;

namespace SITA.Maui;

public partial class SignUpPage : ContentPage
{
    private readonly Storage<User> _userStorage;

    public SignUpPage()
    {
        InitializeComponent();
        _userStorage = MauiProgram.AppStorage.GetStorage<User>();
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        string cpf = CpfEntry.Text?.Trim();
        string password = PasswordEntry.Text;
        string confirm = ConfirmPassEntry.Text;

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(password))
        {
            ShowError("Preencha todos os campos obrigatórios.");
            return;
        }

        if (password != confirm)
        {
            ShowError("As senhas não coincidem.");
            return;
        }

        if (!PasswordHandler.IsValidPassword(password))
        {
            ShowError("Senha fraca. Use mais de 6 caracteres, uma maiúscula e um número.");
            return;
        }

        if (_userStorage.GetData(name) != null)
        {
            ShowError("Já existe um usuário com esse nome.");
            return;
        }

        var newUser = new User
        {
            Name = name,
            CPF = cpf,
            Senha = PasswordHandler.HashPassword(password),
            dataNascimento = NascimentoEntry.Text?.Trim(),
            AccessType = new AccessType { Level = 0 }
        };

        if (int.TryParse(IdadeEntry.Text, out int idade))
            newUser.Idade = idade;

        _userStorage.AddData(name, newUser);

        SuccessLabel.Text = "Conta criada! Redirecionando...";
        SuccessLabel.IsVisible = true;
        ErrorLabel.IsVisible = false;

        await Task.Delay(1200);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
        SuccessLabel.IsVisible = false;
    }
}