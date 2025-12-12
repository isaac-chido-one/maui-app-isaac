using MauiAppIsaac.Views;

namespace MauiAppIsaac
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PatientView), typeof(PatientView));
            Routing.RegisterRoute(nameof(DisplayPatientsView), typeof(DisplayPatientsView));
        }
    }
}
