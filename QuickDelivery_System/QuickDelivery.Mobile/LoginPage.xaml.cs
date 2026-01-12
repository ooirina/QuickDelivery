using QuickDelivery.Mobile.Models;
namespace QuickDelivery.Mobile;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // validări email/parolă
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            !EmailEntry.Text.Contains("@") || !EmailEntry.Text.Contains("."))
        {
            await DisplayAlert("Eroare", "Introdu un email valid", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Eroare", "Introdu parola!", "OK");
            return;
        }

        var service = new Data.RestService();
        var token = await service.LoginAsync(EmailEntry.Text, PasswordEntry.Text);

        if (!string.IsNullOrWhiteSpace(token))
        {
            // ✅ Stocăm token-ul și flag-ul de login
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("AuthToken", token);

            // schimbăm MainPage către AppShell
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current.MainPage = new AppShell();
            });
        }
        else
        {
            await DisplayAlert("Eroare", "Date de logare incorecte!", "OK");
        }
    }


    private async void OnGoToRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
