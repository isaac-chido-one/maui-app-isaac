using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views;

public partial class AlumnoView : ContentPage
{
    public AlumnoView()
    {
        BindingContext = App.Current.Services.GetRequiredService<AlumnoViewModel>();
        InitializeComponent();
    }
}
