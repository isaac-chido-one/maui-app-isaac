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

public partial class AlumnosViewModel : ObservableObject
{
    private readonly IAlumnos _alumnosservice;

    private readonly IDialogService _dialog;

    public ObservableCollection<AlumnoModel> Alumnos { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReady))]
    private bool isLoading;

    [ObservableProperty]
    public bool isRefreshing = false;

    public bool IsReady => !IsLoading;

    public AlumnosViewModel()
    {
        _alumnosservice = App.Current.Services.GetService<IAlumnos>();
        _dialog = App.Current.Services.GetService<IDialogService>();
        Task.Run(async () => await ListarAlumnos());
    }


    [RelayCommand]
    public async Task ListarAlumnos()
    {
        IsLoading = true;
        Alumnos.Clear();
        var lista = await _alumnosservice.GetAll();

        foreach (var item in lista)
        {
            Alumnos.Add(item);
        }

        IsLoading = false;
        isRefreshing = false;
    }

    [RelayCommand]
    public async Task EditarAlumno(AlumnoModel alumno)
    {
        await Shell.Current.GoToAsync($"/AlumnoView?Id={alumno.Id}&Nombre={alumno.Nombre}&Apellido={alumno.Apellido}", false);
    }

    [RelayCommand]
    public async Task EliminarAlumno(AlumnoModel alumno)
    {
        IsLoading = true;
        var res = await _dialog.ShowAlertAsync("Eliminar", $"¿Deseas eliminar el registro {alumno.Id}?", "Aceptar", "Cancelar");

        if (!res)
        {
            return;
        }

        await _alumnosservice.DeleteAlumno(alumno);
        await ListarAlumnos();
    }

    [RelayCommand]
    public async Task AddNew()
    {
        await Shell.Current.Navigation.PushAsync(new AlumnoView(), false);
    }
}
