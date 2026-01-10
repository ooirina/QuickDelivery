using QuickDelivery.Mobile.Models;
using QuickDelivery.Mobile.Data;

namespace QuickDelivery.Mobile;

[QueryProperty(nameof(RestaurantId), "restaurantId")]
public partial class ProdusePage : ContentPage
{
    public int RestaurantId { get; set; }

    public ProdusePage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var service = new RestService();
        var produse = await service.GetProduseByRestaurantAsync(RestaurantId);

        if (produse == null || produse.Count == 0)
        {
            await DisplayAlert("Info", "Nu sunt produse pentru acest restaurant", "OK");
            return;
        }

        produseList.ItemsSource = produse;
    }

    async void OnAddToCartClicked(object sender, EventArgs e)
    {
        var produs = (sender as Button)?.CommandParameter as Produs;
        if (produs == null) return;

        // Luăm produsele existente în coș din SQLite
        var itemsInCart = await App.Database.GetItemsAsync();

        // Dacă coșul nu e gol, verificăm dacă produsul nou e de la același restaurant
        if (itemsInCart.Count > 0)
        {
            if (itemsInCart[0].RestaurantId != RestaurantId)
            {
                bool answer = await DisplayAlert("Coș mixt",
                    "Ai deja produse de la alt restaurant. Vrei să golești coșul și să adaugi acest produs?",
                    "Da", "Nu");

                if (answer)
                {
                    // Golim coșul vechi
                    foreach (var item in itemsInCart)
                        await App.Database.DeleteItemAsync(item);
                }
                else return;
            }
        }

        // Adăugăm produsul nou
        var itemCos = new CartItem
        {
            ProdusId = produs.Id,
            RestaurantId = RestaurantId,
            RestaurantName = "Nume Restaurant",
            Nume = produs.Nume,
            Pret = (decimal)produs.Pret,
            Cantitate = 1
        };

        await App.Database.SaveItemAsync(itemCos);
        await DisplayAlert("Succes", "Produs adăugat!", "OK");
    }
}
