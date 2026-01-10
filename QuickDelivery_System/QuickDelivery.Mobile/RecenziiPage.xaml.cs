using QuickDelivery.Mobile.Models;
using QuickDelivery.Mobile.Data;

namespace QuickDelivery.Mobile;

public partial class RecenziiPage : ContentPage
{
    int _restaurantId;

    public RecenziiPage(int restaurantId, string restaurantName)
    {
        InitializeComponent();
        _restaurantId = restaurantId;
        lblRestaurant.Text = $"Recenzii pentru {restaurantName}";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var service = new RestService();
        var recenzii = await service.GetRecenziiByRestaurantAsync(_restaurantId);

        if (recenzii == null || recenzii.Count == 0)
        {
            await DisplayAlert("Info", "Acest restaurant nu are încă recenzii.", "OK");
            return;
        }

        reviewsList.ItemsSource = recenzii;
    }
}