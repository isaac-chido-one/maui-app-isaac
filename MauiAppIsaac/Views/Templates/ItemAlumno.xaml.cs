using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac.Views.Templates;

public partial class ItemAlumno : ContentView
{
    private readonly AlumnosViewModel viewmodel;
    public ItemAlumno()
    {
        try
        {
            viewmodel = App.Current.Services.GetService<AlumnosViewModel>();
            InitializeComponent();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}