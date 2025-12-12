using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views;

public partial class PatientView : ContentPage
{
    public PatientView()
    {
        BindingContext = App.Current.Services.GetRequiredService<PatientViewModel>();
        InitializeComponent();
    }
}
