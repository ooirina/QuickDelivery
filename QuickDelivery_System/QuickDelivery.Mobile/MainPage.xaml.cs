using QuickDelivery.Mobile.Data;
using QuickDelivery.Mobile.Models;

namespace QuickDelivery.Mobile;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var service = new RestService();
            var restaurante = await service.GetRestaurantsAsync();

            listView.ItemsSource = restaurante;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", ex.Message, "OK");
        }
    }

    // **Fix final: click funcțional pe restaurant**
    private async void OnRestaurantTapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        if (frame == null) return;

        var restaurant = frame.BindingContext as Restaurant;
        if (restaurant == null) return;

        // Navigare către ProdusePage
        await Shell.Current.GoToAsync($"ProdusePage?restaurantId={restaurant.Id}");
    }
}
