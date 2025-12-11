using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views;

public partial class ListadoAlumnosView : ContentPage
{
    public ListadoAlumnosView()
    {
        BindingContext = App.Current.Services.GetRequiredService<AlumnosViewModel>();
        InitializeComponent();
    }
}
