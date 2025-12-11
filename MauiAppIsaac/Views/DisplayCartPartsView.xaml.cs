using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views;

public partial class DisplayCartPartsView : ContentPage
{
    public DisplayCartPartsView()
    {
        BindingContext = App.Current.Services.GetRequiredService<CarPartsViewModel>();
        InitializeComponent();
    }
}
