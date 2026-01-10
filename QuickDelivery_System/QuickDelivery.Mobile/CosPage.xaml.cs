using QuickDelivery.Mobile.Models;
//using Plugin.LocalNotification;
namespace QuickDelivery.Mobile;

public partial class CosPage : ContentPage
{
    public CosPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RefreshList();
    }

    private async Task RefreshList()
    {
        var items = await App.Database.GetItemsAsync();
        cosList.ItemsSource = items;
    }

    // Buton crește cantitatea
    private async void OnIncreaseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.CommandParameter as CartItem;
        if (item == null) return;

        item.Cantitate += 1;
        await App.Database.UpdateItemAsync(item);

        await RefreshList();
    }

    // Buton scade cantitatea
    private async void OnDecreaseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.CommandParameter as CartItem;
        if (item == null) return;

        if (item.Cantitate > 1)
        {
            item.Cantitate -= 1;
            await App.Database.UpdateItemAsync(item);
        }
        else
        {
            // Dacă cantitatea ajunge la 0, șterge item-ul
            await App.Database.DeleteItemAsync(item);
        }

        await RefreshList();
    }

    // Șterge item din coș
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button.CommandParameter as CartItem;

        if (item != null)
        {
            await App.Database.DeleteItemAsync(item);
            await RefreshList();
        }
    }

    // Finalizare comandă
    async void OnCheckoutClicked(object sender, EventArgs e)
    {
        var items = await App.Database.GetItemsAsync();
        if (items.Count == 0)
        {
            await DisplayAlert("Coș gol", "Nu poți finaliza comanda. Coșul este gol.", "OK");
            return;
        }

        // GENERARE ID NUMERIC (6 cifre)
        Random rand = new Random();
        string orderId = rand.Next(100000, 999999).ToString();

        foreach (var i in items)
        {
            var istoricNou = new OrderHistory
            {
                OrderGroupId = orderId, 
                RestaurantName = i.RestaurantName,
                ProductName = i.Nume,
                Price = i.Pret, 
                OrderDate = DateTime.Now
            };
            await App.Database.SaveHistoryAsync(istoricNou);
            await App.Database.DeleteItemAsync(i); 
        }

        await DisplayAlert("Comandă Finalizată", $"Comanda ta cu ID-ul {orderId} a fost trimisă!", "OK");
        await RefreshList();
    }
}
