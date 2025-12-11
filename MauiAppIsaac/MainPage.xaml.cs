using MauiAppIsaac.Interfaces;
using MauiAppIsaac.ViewModels;

namespace MauiAppIsaac
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = App.Current.Services.GetRequiredService<TestViewModel>();
            InitializeComponent();
        }
    }
}
