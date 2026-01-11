namespace QuickDelivery.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProdusePage), typeof(ProdusePage));

        }
    }
}
