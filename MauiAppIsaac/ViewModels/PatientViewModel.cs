using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppIsaac.Entities;
using MauiAppIsaac.Interfaces;
using MauiAppIsaac.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.ViewModels;

[QueryProperty("Id", "Id")]
[QueryProperty("FirstName", "FirstName")]
[QueryProperty("LastName", "LastName")]
[QueryProperty("Age", "Age")]
[QueryProperty("Weight", "Weight")]
[QueryProperty("Height", "Height")]
[QueryProperty("Gender", "Gender")]
[QueryProperty("Level", "Level")]
public partial class PatientViewModel : ObservableValidator
{
    private readonly IPatients _service;

    public ObservableCollection<string> Errors { get; set; } = new();

    [ObservableProperty]
    private string result = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEnabled))]
    private bool isVisible;

    public bool IsEnabled => !isVisible;

    [ObservableProperty]
    private int id = 0;

    private int gender = 0;

    private int level = 0;

    private string firstName = string.Empty;

    private int age = 0;

    private string lastName = string.Empty;

    private double weight = 0.0;

    private double height = 0.0;

    [Required(ErrorMessage = "El género es requerido")]
    public int Gender
    {
        get => gender;
        set => SetProperty(ref gender, value, true);
    }

    [Required(ErrorMessage = "La nivel de actividad es requerido")]
    public int Level
    {
        get => level;
        set => SetProperty(ref level, value, true);
    }

    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(255)]
    public string FirstName
    {
        get => firstName;
        set => SetProperty(ref firstName, value, true);
    }

    [Required(ErrorMessage = "La edad es requerida")]
    [Range(0, 100, ErrorMessage = "El rango de edad es incorrecto")]
    public int Age
    {
        get => age;
        set => SetProperty(ref age, value, true);
    }

    [MaxLength(255)]
    [Required(ErrorMessage = "El apellido es requerido")]
    public string LastName
    {
        get => lastName;
        set => SetProperty(ref lastName, value, true);
    }

    [Required(ErrorMessage = "El peso es requerido")]
    public double Weight
    {
        get => weight;
        set => SetProperty(ref weight, value, true);
    }

    [Required(ErrorMessage = "La estatura es requerida")]
    public double Height
    {
        get => height;
        set => SetProperty(ref height, value, true);
    }


    public List<ItemEntity> Levels { get; set; }

    public List<ItemEntity> Genders { get; set; }

    public PatientViewModel()
    {
        _service = App.Current.Services.GetRequiredService<IPatients>();
        Levels = new List<ItemEntity>();
        Genders = new List<ItemEntity>();

        for (int i = 0; i < PatientModel.LEVELS.Length; i++)
        {
            Levels.Add(new ItemEntity
            {
                Id = i,
                Name = PatientModel.LEVELS[i]
            });
        }

        for (int i = 0; i < PatientModel.GENDERS.Length; i++)
        {
            Genders.Add(new ItemEntity
            {
                Id = i,
                Name = PatientModel.GENDERS[i]
            });
        }
    }

    [RelayCommand]
    public async Task StorePatient()
    {
        IsBusy = true;
        IsVisible = false;
        ValidateAllProperties();

        Errors.Clear();
        GetErrors(nameof(Gender)).ToList().ForEach(f => Errors.Add("Género: " + f.ErrorMessage));
        GetErrors(nameof(Level)).ToList().ForEach(f => Errors.Add("Nivel de actividad: " + f.ErrorMessage));
        GetErrors(nameof(FirstName)).ToList().ForEach(f => Errors.Add("Nombre: " + f.ErrorMessage));
        GetErrors(nameof(Age)).ToList().ForEach(f => Errors.Add("Edad: " + f.ErrorMessage));
        GetErrors(nameof(LastName)).ToList().ForEach(f => Errors.Add("Apellido: " + f.ErrorMessage));
        GetErrors(nameof(Weight)).ToList().ForEach(f => Errors.Add("Peso: " + f.ErrorMessage));
        GetErrors(nameof(Height)).ToList().ForEach(f => Errors.Add("Estatura: " + f.ErrorMessage));

        IsBusy = false;

        if (Errors.Count > 0)
        {
            return;
        }

        try
        {
            if (Id == 0)
            {
                Id = await _service.InsertPatient(new PatientModel()
                {
                    Gender = Gender,
                    Level = Level,
                    FirstName = FirstName,
                    Age = Age,
                    LastName = LastName,
                    Weight = Weight,
                    Height = Height
                });
            }
            else
            {
                await _service.UpdatePatient(new PatientModel()
                {
                    Id = Id,
                    Gender = Gender,
                    Level = Level,
                    FirstName = FirstName,
                    Age = Age,
                    LastName = LastName,
                    Weight = Weight,
                    Height = Height
                });
            }
        }
        catch (Exception ex)
        {
            Errors.Add(ex.Message);
        }

        Result = $" Registro id: {Id}";
        IsBusy = false;
        IsVisible = true;

        await Task.Delay(2500);
        await Shell.Current.Navigation.PopToRootAsync();

        // Intento de refrescar la lista en el ViewModel de la vista raíz (ListPatientsView)
        var rootPage = Shell.Current.CurrentPage;
        if (rootPage?.BindingContext is PatientsViewModel patientsVm)
        {
            await patientsVm.DisplayPatients();
        }
        else
        {
            // Buscar en la pila de navegación por si la página raíz es distinta
            var pageWithVm = Shell.Current.Navigation.NavigationStack.FirstOrDefault(p => p.BindingContext is PatientsViewModel);
            if (pageWithVm?.BindingContext is PatientsViewModel patientsVm2)
            {
                await patientsVm2.DisplayPatients();
            }
        }
    }
}
