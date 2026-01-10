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
        var istoric = await App.Database.GetHistoryAsync();
        historyList.ItemsSource = istoric;
    }
}