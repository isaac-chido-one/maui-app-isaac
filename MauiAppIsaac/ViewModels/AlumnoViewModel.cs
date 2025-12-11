using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppIsaac.Interfaces;
using MauiAppIsaac.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.ViewModels;

[QueryProperty("Nombre", "Nombre")]
[QueryProperty("Apellido", "Apellido")]
[QueryProperty("Id", "Id")]
public partial class AlumnoViewModel : ObservableValidator
{
    private readonly IAlumnos alumno_service;

    public ObservableCollection<string> Errores { get; set; } = new();

    [ObservableProperty]
    private string resultado;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEnabled))]
    private bool isVisible;

    public bool IsEnabled => !isVisible;

    [ObservableProperty]
    private int id = 0;

    private string apellido = string.Empty;

    [Required(ErrorMessage = "El campo apellido es obligatorio")]
    [MaxLength(30)]
    public string Apellido
    {
        get => apellido;
        set => SetProperty(ref apellido, value, true);
    }

    private string nombre = string.Empty;

    [Required(ErrorMessage = "El campo nombre es obligatorio")]
    [MaxLength(30)]
    public string Nombre
    {
        get => nombre;
        set => SetProperty(ref nombre, value, true);
    }

    public AlumnoViewModel()
    {
        alumno_service = App.Current.Services.GetRequiredService<IAlumnos>();
    }

    [RelayCommand]
    public async Task GuardarAlumno()
    {
        IsBusy = true;
        IsVisible = false;
        ValidateAllProperties();

        Errores.Clear();
        GetErrors(nameof(Nombre)).ToList().ForEach(f => Errores.Add("Nombre: " + f.ErrorMessage));
        GetErrors(nameof(Apellido)).ToList().ForEach(f => Errores.Add("Apellido: " + f.ErrorMessage));
        IsBusy = false;

        if (Errores.Count > 0)
        {
            return;
        }

        if (Id == 0)
        {
            Id = await alumno_service.InsertarAlumno(new AlumnoModel()
            {
                Nombre = Nombre,
                Apellido = Apellido
            });
        }
        else
        {
            await alumno_service.UpdateAlumno(new AlumnoModel()
            {
                Id = Id,
                Nombre = Nombre,
                Apellido = Apellido
            });
        }

        Resultado = $" Registro id: {Id}";
        IsBusy = false;
        IsVisible = true;

        await Task.Delay(2500);
        await Shell.Current.Navigation.PopToRootAsync();

        // Intento de refrescar la lista en el ViewModel de la vista raíz (ListadoAlumnosView)
        var rootPage = Shell.Current.CurrentPage;
        if (rootPage?.BindingContext is AlumnosViewModel alumnosVm)
        {
            await alumnosVm.ListarAlumnos();
        }
        else
        {
            // Buscar en la pila de navegación por si la página raíz es distinta
            var pageWithVm = Shell.Current.Navigation.NavigationStack.FirstOrDefault(p => p.BindingContext is AlumnosViewModel);
            if (pageWithVm?.BindingContext is AlumnosViewModel alumnosVm2)
            {
                await alumnosVm2.ListarAlumnos();
            }
        }
    }
}
