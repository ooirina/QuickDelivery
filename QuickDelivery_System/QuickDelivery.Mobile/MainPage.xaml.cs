using QuickDelivery.Mobile.Data;
using QuickDelivery.Mobile.Models;
using Microsoft.Maui.Devices.Sensors;

namespace QuickDelivery.Mobile;

public partial class MainPage : ContentPage
{
    List<Restaurant> toateRestaurantele = new List<Restaurant>();

    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            // --- TEST URL DIRECT ---
            var testUrl = "http://10.0.2.2:5132/images/2bcd603b-47c1-4ce1-89f5-5e67feb5af19_185677-marty-restaurant.jpg";

            using var client = new HttpClient();
            var response = await client.GetAsync(testUrl);

            // Folosim Debug.WriteLine pentru a vedea rezultatul în Output
            System.Diagnostics.Debug.WriteLine($"Status cod test URL: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine("Imaginea este accesibilă!");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Imaginea NU se poate accesa!");
            }

            // --- ÎNCĂRCARE RESTAURANTE ---
            var service = new RestService();
            toateRestaurantele = await service.GetRestaurantsAsync();


            foreach (var r in toateRestaurantele)
            {
                // fallback dacă lipsește imagine
                if (string.IsNullOrWhiteSpace(r.ImagineUrl))
                {
                    r.ImagineUrl = "https://via.placeholder.com/60";
                }
            }


            listView.ItemsSource = toateRestaurantele;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", ex.Message, "OK");
        }
    }


   


    private async Task<Location?> GetUserLocationAsync()
    {
        var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
            return null;

        return await Geolocation.GetLastKnownLocationAsync()
               ?? await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
    }

    private double DistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        return Location.CalculateDistance(lat1, lon1, lat2, lon2, DistanceUnits.Kilometers);
    }

   

    private async void OnRestaurantTapped(object sender, EventArgs e)
    {
        if ((sender as Frame)?.BindingContext is not Restaurant restaurant)
            return;

        await Shell.Current.GoToAsync($"ProdusePage?restaurantId={restaurant.Id}&restaurantName={restaurant.Nume}");
    }
}
