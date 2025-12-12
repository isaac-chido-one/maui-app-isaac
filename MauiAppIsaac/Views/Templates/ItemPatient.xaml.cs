using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views.Templates;

public partial class ItemPatient : ContentView
{
    private readonly PatientsViewModel? viewmodel;
    public ItemPatient()
    {
        try
        {
            viewmodel = App.Current.Services.GetService<PatientsViewModel>();
            InitializeComponent();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
