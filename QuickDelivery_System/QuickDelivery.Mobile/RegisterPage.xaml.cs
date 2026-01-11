using QuickDelivery.Mobile.Models;
namespace QuickDelivery.Mobile;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (passwordEntry.Text != confirmPasswordEntry.Text)
        {
            await DisplayAlert("Eroare", "Parolele nu coincid!", "OK");
            return;
        }

        var registerData = new
        {
            Email = emailEntry.Text,
            Password = passwordEntry.Text
        };

        using var client = new HttpClient();
        var json = System.Text.Json.JsonSerializer.Serialize(registerData);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        // Corectat ruta către Auth/register (nu Account)
        var response = await client.PostAsync("http://10.0.2.2:5132/api/Auth/register", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Succes", "Cont creat! Acum te poți loga.", "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "Înregistrarea a eșuat. Verifică datele.", "OK");
        }
    }

    // ADAUGĂ ACEASTĂ METODĂ PENTRU A ȘTERGE EROAREA:
    private async void OnBackToLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}