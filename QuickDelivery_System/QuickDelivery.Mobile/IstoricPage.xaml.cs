using QuickDelivery.Mobile.Models;
namespace QuickDelivery.Mobile;

public partial class IstoricPage : ContentPage
{
	public IstoricPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var istoricRaw = await App.Database.GetHistoryAsync();

        var grupate = istoricRaw
            .OrderByDescending(h => h.OrderDate) // Cele mai noi comenzi sus
            .GroupBy(h => h.OrderGroupId)        // Grupare după ID-ul unic de comandă
            .Select(g => new OrderGroup(
                g.First().RestaurantName,
                g.First().OrderDate,
                g.ToList()))
            .ToList();

        historyList.ItemsSource = grupate;
    }
}