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

[QueryProperty("Id", "Id")]
[QueryProperty("Category", "Category")]
[QueryProperty("Brand", "Brand")]
[QueryProperty("Model", "Model")]
[QueryProperty("Year", "Year")]
[QueryProperty("Description", "Description")]
public partial class CarPartViewModel : ObservableValidator
{
    private readonly ICarParts _service;

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

    private int category = 0;

    private int brand = 0;

    private string model = string.Empty;

    private int year = 0;

    private string description = string.Empty;

    [Required(ErrorMessage = "La categoría es requerida")]
    public int Category
    {
        get => category;
        set => SetProperty(ref category, value, true);
    }

    [Required(ErrorMessage = "La marca es requerida")]
    public int Brand
    {
        get => brand;
        set => SetProperty(ref brand, value, true);
    }

    [Required(ErrorMessage = "El model es requerido")]
    [MaxLength(255)]
    public string Model
    {
        get => model;
        set => SetProperty(ref model, value, true);
    }

    [Required(ErrorMessage = "El año es requerido")]
    public int Year
    {
        get => year;
        set => SetProperty(ref year, value, true);
    }

    [MaxLength(255)]
    [Required(ErrorMessage = "La descripción es requerida")]
    public string Description
    {
        get => description;
        set => SetProperty(ref description, value, true);
    }

    public CarPartViewModel()
    {
        _service = App.Current.Services.GetRequiredService<ICarParts>();
    }

    [RelayCommand]
    public async Task StoreCarPart()
    {
        IsBusy = true;
        IsVisible = false;
        ValidateAllProperties();

        Errors.Clear();
        GetErrors(nameof(Category)).ToList().ForEach(f => Errors.Add("Categoría: " + f.ErrorMessage));
        GetErrors(nameof(Brand)).ToList().ForEach(f => Errors.Add("Marca: " + f.ErrorMessage));
        GetErrors(nameof(Model)).ToList().ForEach(f => Errors.Add("Modelo: " + f.ErrorMessage));
        GetErrors(nameof(Year)).ToList().ForEach(f => Errors.Add("Año: " + f.ErrorMessage));
        GetErrors(nameof(Description)).ToList().ForEach(f => Errors.Add("Descripción: " + f.ErrorMessage));

        IsBusy = false;

        if (Errors.Count > 0)
        {
            return;
        }

        if (Id == 0)
        {
            Id = await _service.InsertCarPart(new CarPartModel()
            {
                Category = Category,
                Brand = Brand,
                Model = Model,
                Year = Year,
                Description = Description
            });
        }
        else
        {
            await _service.UpdateCarPart(new CarPartModel()
            {
                Id = Id,
                Category = Category,
                Brand = Brand,
                Model = Model,
                Year = Year,
                Description = Description
            });
        }

        Result = $" Registro id: {Id}";
        IsBusy = false;
        IsVisible = true;

        await Task.Delay(2500);
        await Shell.Current.Navigation.PopToRootAsync();

        // Intento de refrescar la lista en el ViewModel de la vista raíz (ListCarPartsView)
        var rootPage = Shell.Current.CurrentPage;
        if (rootPage?.BindingContext is CarPartsViewModel carPartsVm)
        {
            await carPartsVm.DisplayCarParts();
        }
        else
        {
            // Buscar en la pila de navegación por si la página raíz es distinta
            var pageWithVm = Shell.Current.Navigation.NavigationStack.FirstOrDefault(p => p.BindingContext is CarPartsViewModel);
            if (pageWithVm?.BindingContext is CarPartsViewModel carPartsVm2)
            {
                await carPartsVm2.DisplayCarParts();
            }
        }
    }
}
