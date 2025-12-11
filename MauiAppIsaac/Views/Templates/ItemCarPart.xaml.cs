using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views.Templates;

public partial class ItemCarPart : ContentView
{
    private readonly CarPartsViewModel? viewmodel;
    public ItemCarPart()
    {
        try
        {
            viewmodel = App.Current.Services.GetService<CarPartsViewModel>();
            InitializeComponent();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
