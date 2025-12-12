using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views;

public partial class DisplayPatientsView : ContentPage
{
    public DisplayPatientsView()
    {
        BindingContext = App.Current.Services.GetRequiredService<PatientsViewModel>();
        InitializeComponent();
    }
}
