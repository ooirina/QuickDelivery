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

        // Reset items source înainte
        listView.ItemsSource = null;
        categoriesList.ItemsSource = null;

        await LoadDataAsync();
    }


    private async Task LoadDataAsync()
    {
        try
        {
            var service = new RestService();

            toateRestaurantele = await service.GetRestaurantsAsync();

            var userLocation = await GetUserLocationAsync();
            if (userLocation != null)
            {
                foreach (var r in toateRestaurantele)
                {
                    r.DistantaKm = DistanceKm(
                        userLocation.Latitude,
                        userLocation.Longitude,
                        r.Latitude,
                        r.Longitude);
                }

                toateRestaurantele = toateRestaurantele
                    .OrderBy(r => r.DistantaKm)
                    .ToList();
            }

            if (toateRestaurantele.Any())
                toateRestaurantele[0].Nume += " 🏆";

            listView.ItemsSource = toateRestaurantele;

            var cats = await service.GetCategoriesAsync() ?? new();
            if (!cats.Any(c => c.Id == 0))
                cats.Insert(0, new Categorie { Id = 0, Nume = "Toate" });

            categoriesList.ItemsSource = cats;
            categoriesList.SelectedItem = cats.First();
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

    private void OnCategoryChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Categorie selected)
            return;

        if (selected.Id == 0) // TOATE
        {
            listView.ItemsSource = toateRestaurantele;
        }
        else
        {
            listView.ItemsSource = toateRestaurantele
                .Where(r => r.CategorieId == selected.Id)
                .ToList();
        }
    }


    private async void OnRestaurantTapped(object sender, EventArgs e)
    {
        if ((sender as Frame)?.BindingContext is not Restaurant restaurant)
            return;

        await Shell.Current.GoToAsync($"ProdusePage?restaurantId={restaurant.Id}&restaurantName={restaurant.Nume}");
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        Preferences.Clear();
        // Forțează reinitializarea aplicației
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }



}
