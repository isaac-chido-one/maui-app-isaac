using MauiAppIsaac.Views;

namespace MauiAppIsaac
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AlumnoView), typeof(AlumnoView));
            Routing.RegisterRoute(nameof(ListadoAlumnosView), typeof(ListadoAlumnosView));
        }
    }
}
