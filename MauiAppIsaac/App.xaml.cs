using MauiAppIsaac.Interfaces;
using MauiAppIsaac.Services;
using MauiAppIsaac.ViewModels;
using MauiAppIsaac.Views;

namespace MauiAppIsaac
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        public App()
        {
            var services = new ServiceCollection();
            Services = ConfigureServices(services);
            InitializeComponent();

            MainPage = new AppShell();
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // ViewModels
            services.AddTransient<CarPartsViewModel>();
            services.AddTransient<CarPartViewModel>();

            // Views
            services.AddSingleton<DisplayCartPartsView>();
            services.AddSingleton<CarPartView>();

            // Services
            services.AddSingleton<ICarParts, CarPartsService>();
            services.AddTransient<IDialogService, DialogService>();

            return services.BuildServiceProvider();
        }
    }
}
