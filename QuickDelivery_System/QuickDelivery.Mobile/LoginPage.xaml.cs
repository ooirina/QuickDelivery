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
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            !EmailEntry.Text.Contains("@") ||
            !EmailEntry.Text.Contains("."))
        {
            await DisplayAlert(
                "Eroare",
                "Introdu un email valid (ex: nume@email.com)",
                "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Eroare", "Introdu parola!", "OK");
            return;
        }

        var service = new Data.RestService();
        bool succes = await service.LoginAsync(
            EmailEntry.Text,
            PasswordEntry.Text);

        if (succes)
        {
            Preferences.Set("IsLoggedIn", true);
            Application.Current.MainPage = new AppShell();
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