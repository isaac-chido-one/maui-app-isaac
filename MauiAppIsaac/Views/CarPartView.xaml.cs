using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views;

public partial class CarPartView : ContentPage
{
    public CarPartView()
    {
        BindingContext = App.Current.Services.GetRequiredService<CarPartViewModel>();
        InitializeComponent();
    }
}
