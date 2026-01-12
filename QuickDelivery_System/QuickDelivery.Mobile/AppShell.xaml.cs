using QuickDelivery.Mobile;
using Microsoft.Maui.Controls;

namespace QuickDelivery.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProdusePage), typeof(ProdusePage));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Preferences.Set("IsLoggedIn", false);
            Preferences.Remove("AuthToken");

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            });
        }
    }
}
