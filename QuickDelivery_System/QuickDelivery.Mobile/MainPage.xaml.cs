using QuickDelivery.Mobile.Data;
using QuickDelivery.Mobile.Models;
using Microsoft.Maui.Devices.Sensors;

namespace QuickDelivery.Mobile;

public partial class MainPage : ContentPage
{
    List<Restaurant> toateRestaurantele = new List<Restaurant>();
    List<Categorie> categorii = new List<Categorie>();

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
            

            // 1️⃣ Încarcă restaurantele
            toateRestaurantele = await service.GetRestaurantsAsync();

            if (!toateRestaurantele.Any())
            {
                await DisplayAlert("Debug", "Nu s-au primit restaurante de la server", "OK");
            }

            // 2️⃣ Set default distanțe
            foreach (var r in toateRestaurantele)
            {
                r.DistantaKm = 0; // fallback dacă nu avem coordonate
            }

            var userLocation = await GetUserLocationAsync();
            if (userLocation != null)
            {
                foreach (var r in toateRestaurantele)
                {
                    if (r.Latitude.HasValue && r.Longitude.HasValue)
                    {
                        r.DistantaKm = DistanceKm(userLocation.Latitude, userLocation.Longitude, r.Latitude.Value, r.Longitude.Value);
                    }
                }
                toateRestaurantele = toateRestaurantele.OrderBy(r => r.DistantaKm).ToList();
            }

            // 🔹 Mark restaurantul cel mai apropiat
            if (toateRestaurantele.Any())
                toateRestaurantele[0].Nume += " 🏆";

            // 3️⃣ Afișează restaurantele
            listView.ItemsSource = toateRestaurantele;

            // 4️⃣ Încarcă categoriile
            categorii = await service.GetCategoriesAsync() ?? new List<Categorie>();
            if (!categorii.Any(c => c.Id == 0))
                categorii.Insert(0, new Categorie { Id = 0, Nume = "Toate" });

            categoriesList.ItemsSource = categorii;
            categoriesList.SelectedItem = categorii.FirstOrDefault();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", ex.Message, "OK");
        }
    }

    private void OnCategoryChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Categorie selected)
            return;

        if (selected.Id == 0) // "Toate"
            listView.ItemsSource = toateRestaurantele;
        else
            listView.ItemsSource = toateRestaurantele
                                    .Where(r => r.CategorieId == selected.Id)
                                    .ToList();
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
