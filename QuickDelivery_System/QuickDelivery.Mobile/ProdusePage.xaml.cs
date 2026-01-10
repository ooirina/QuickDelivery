using QuickDelivery.Mobile.Models;
using QuickDelivery.Mobile.Data;
using Plugin.LocalNotification;
namespace QuickDelivery.Mobile;

[QueryProperty(nameof(RestaurantId), "restaurantId")]
[QueryProperty(nameof(RestaurantName), "restaurantName")]
public partial class ProdusePage : ContentPage
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    List<Produs> toateProdusele = new List<Produs>();

    public ProdusePage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var service = new RestService();

        // 1. Luăm toate produsele restaurantului
        toateProdusele = await service.GetProduseByRestaurantAsync(RestaurantId);
        produseList.ItemsSource = toateProdusele;

        // 2. Luăm toate categoriile de pe server
        var toateCategoriile = await service.GetCategoriesAsync();

        // 3. FILTRARE: Păstrăm doar categoriile care au cel puțin un produs în acest restaurant
        var categoriiSpecifice = toateCategoriile
            .Where(cat => toateProdusele.Any(p => p.CategorieId == cat.Id))
            .ToList();

        categoriesList.ItemsSource = categoriiSpecifice;
    }

    private void OnCategoryChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Categorie selected) return;

        if (selected.Id == 0)
            produseList.ItemsSource = toateProdusele;
        else
            produseList.ItemsSource = toateProdusele.Where(p => p.CategorieId == selected.Id).ToList();
    }

    async void OnAddToCartClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is not Produs produs) return;

        var itemsInCart = await App.Database.GetItemsAsync();
        if (itemsInCart.Count > 0 && itemsInCart[0].RestaurantId != RestaurantId)
        {
            bool answer = await DisplayAlert("Coș mixt", "Goliți coșul pentru acest restaurant?", "Da", "Nu");
            if (answer)
                foreach (var item in itemsInCart) await App.Database.DeleteItemAsync(item);
            else return;
        }

        await App.Database.SaveItemAsync(new CartItem
        {
            ProdusId = produs.Id,
            RestaurantId = RestaurantId,
            RestaurantName = RestaurantName,
            Nume = produs.Nume,
            Pret = (decimal)produs.Pret,
            Cantitate = 1
        });

        //trimitere notificare
        var request = new NotificationRequest
        {
            NotificationId = 1000,
            Title = "Produs adăugat!",
            Description = $"{produs.Nume} a fost adăugat în coșul tău.",
            BadgeNumber = 1,
            Schedule = { NotifyTime = DateTime.Now.AddSeconds(1) } // Apare după o secundă
        };
        await LocalNotificationCenter.Current.Show(request);
        await DisplayAlert("Succes", $"{produs.Nume} a fost adăugat!", "OK");
    }

    private async void OnViewReviewsClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new RecenziiPage(RestaurantId, RestaurantName));
}