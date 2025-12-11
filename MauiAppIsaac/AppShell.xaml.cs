using MauiAppIsaac.Views;

namespace MauiAppIsaac
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CarPartView), typeof(CarPartView));
            Routing.RegisterRoute(nameof(DisplayCartPartsView), typeof(DisplayCartPartsView));
        }
    }
}
