using MauiAppIsaac.Views;

namespace MauiAppIsaac
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(AlumnoView), typeof(AlumnoView));
            //Routing.RegisterRoute(nameof(ListadoAlumnosView), typeof(ListadoAlumnosView));
            Routing.RegisterRoute(nameof(CarPartView), typeof(CarPartView));
            Routing.RegisterRoute(nameof(DisplayCartPartsView), typeof(DisplayCartPartsView));
        }
    }
}
