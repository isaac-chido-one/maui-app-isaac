using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppIsaac.Interfaces;
using MauiAppIsaac.Models;
using MauiAppIsaac.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.ViewModels;

public partial class CarPartsViewModel : ObservableObject
{
    private readonly ICarParts _service;

    private readonly IDialogService _dialog;

    public ObservableCollection<CarPartModel> CarParts { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReady))]
    private bool isLoading;

    [ObservableProperty]
    public bool isRefreshing = false;

    public bool IsReady => !IsLoading;

    public CarPartsViewModel()
    {
        _service = App.Current.Services.GetService<ICarParts>();
        _dialog = App.Current.Services.GetService<IDialogService>();
        Task.Run(async () => await DisplayCarParts());
    }


    [RelayCommand]
    public async Task DisplayCarParts()
    {
        IsLoading = true;
        CarParts.Clear();
        var list = await _service.GetAll();

        foreach (var item in list)
        {
            CarParts.Add(item);
        }

        IsLoading = false;
        isRefreshing = false;
    }

    [RelayCommand]
    public async Task EditCarPart(CarPartModel carPart)
    {
        await Shell.Current.GoToAsync($"/CarPartView?Id={carPart.Id}&Category={carPart.Category}&Brand={carPart.Brand}&Model={carPart.Model}&Year={carPart.Year}&Description={carPart.Description}", false);
    }

    [RelayCommand]
    public async Task DeleteCarPart(CarPartModel carPart)
    {
        IsLoading = true;
        var res = await _dialog.ShowAlertAsync("Eliminar", $"¿Deseas eliminar la autoparte {carPart.Id}?", "Aceptar", "Cancelar");

        if (!res)
        {
            return;
        }

        await _service.DeleteCarPart(carPart);
        await DisplayCarParts();
    }

    [RelayCommand]
    public async Task AddNew()
    {
        await Shell.Current.Navigation.PushAsync(new CarPartView(), false);
    }
}
