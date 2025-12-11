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
            services.AddSingleton<IFunctions, Functions>();

            // ViewModels
            services.AddTransient<TestViewModel>();
            services.AddTransient<AlumnosViewModel>();
            services.AddTransient<AlumnoViewModel>();
            services.AddTransient<CarPartsViewModel>();
            services.AddTransient<CarPartViewModel>();

            // Views
            services.AddSingleton<ListadoAlumnosView>();
            services.AddSingleton<AlumnoView>();
            services.AddSingleton<DisplayCartPartsView>();
            services.AddSingleton<CarPartView>();

            // Services
            services.AddSingleton<IAlumnos, AlumnosService>();
            services.AddSingleton<ICarParts, CarPartsService>();
            services.AddTransient<IDialogService, DialogService>();

            return services.BuildServiceProvider();
        }
    }
}
