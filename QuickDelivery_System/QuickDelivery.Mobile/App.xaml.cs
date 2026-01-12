using QuickDelivery.Mobile.Data;
using Microsoft.Maui.Controls;
using System.IO;

namespace QuickDelivery.Mobile
{
    public partial class App : Application
    {
        static Database database;

        // Acces global la baza de date locală
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "QuickDelivery.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            // Dacă utilizatorul este deja logat, mergem direct în AppShell
            if (Preferences.Get("IsLoggedIn", false) &&
                !string.IsNullOrWhiteSpace(Preferences.Get("AuthToken", string.Empty)))
            {
                MainPage = new AppShell();
            }
            else
            {
                // Altfel, îl trimitem la pagina de login
                MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
