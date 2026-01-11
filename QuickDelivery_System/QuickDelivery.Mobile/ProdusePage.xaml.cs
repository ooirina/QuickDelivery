using QuickDelivery.Mobile.Models;
using QuickDelivery.Mobile.Data;

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
        if (!string.IsNullOrEmpty(RestaurantName)) this.Title = $"Meniu {RestaurantName}";

        var service = new RestService();
        var categorii = await service.GetCategoriesByRestaurantAsync(RestaurantId);
        categoriesList.ItemsSource = categorii;

        var produse = await service.GetProduseByRestaurantAsync(RestaurantId);
        if (produse == null || produse.Count == 0)
        {
            await DisplayAlert("Info", "Nu sunt produse pentru acest restaurant", "OK");
            return;
        }
        toateProdusele = produse;
        produseList.ItemsSource = produse;
    }

    async void OnAddToCartClicked(object sender, EventArgs e)
    {
        var produs = (sender as Button)?.CommandParameter as Produs;
        if (produs == null) return;

        var itemsInCart = await App.Database.GetItemsAsync();
        if (itemsInCart.Count > 0 && itemsInCart[0].RestaurantId != RestaurantId)
        {
            bool answer = await DisplayAlert("Coș mixt", "Ai deja produse de la alt restaurant. Golești coșul?", "Da", "Nu");
            if (answer) { foreach (var item in itemsInCart) await App.Database.DeleteItemAsync(item); }
            else return;
        }

        await App.Database.SaveItemAsync(new CartItem
        {
            ProdusId = produs.Id,
            RestaurantId = RestaurantId,
            RestaurantName = this.RestaurantName,
            Nume = produs.Nume,
            Pret = (decimal)produs.Pret,
            Cantitate = 1
        });
        await DisplayAlert("Succes", $"{produs.Nume} a fost adăugat în coș!", "OK");
    }

    private void OnCategoryChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Categorie;
        if (selected == null) return;
        produseList.ItemsSource = toateProdusele.Where(p => p.CategorieId == selected.Id).ToList();
    }

    private async void OnViewReviewsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RecenziiPage(RestaurantId, RestaurantName));
    }
}