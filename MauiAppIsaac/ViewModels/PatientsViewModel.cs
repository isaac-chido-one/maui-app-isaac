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

public partial class PatientsViewModel : ObservableObject
{
    private readonly IPatients _service;

    private readonly IDialogService _dialog;

    public ObservableCollection<PatientModel> Patients { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReady))]
    private bool isLoading;

    [ObservableProperty]
    public bool isRefreshing = false;

    public bool IsReady => !IsLoading;

    public PatientsViewModel()
    {
        _service = App.Current.Services.GetService<IPatients>();
        _dialog = App.Current.Services.GetService<IDialogService>();
        Task.Run(async () => await DisplayPatients());
    }


    [RelayCommand]
    public async Task DisplayPatients()
    {
        IsLoading = true;
        Patients.Clear();
        var list = await _service.GetAll();

        foreach (var item in list)
        {
            Patients.Add(item);
        }

        IsLoading = false;
        isRefreshing = false;
    }

    [RelayCommand]
    public async Task EditPatient(PatientModel patient)
    {
        await Shell.Current.GoToAsync($"/PatientView?Id={patient.Id}&Gender={patient.Gender}&Level={patient.Level}&FirstName={patient.FirstName}&Age={patient.Age}&LastName={patient.LastName}&Weight={patient.Weight}&Height={patient.Height}", false);
    }

    [RelayCommand]
    public async Task DeletePatient(PatientModel patient)
    {
        IsLoading = true;
        var res = await _dialog.ShowAlertAsync("Eliminar", $"¿Deseas eliminar al paciente {patient.Id}?", "Aceptar", "Cancelar");

        if (!res)
        {
            return;
        }

        await _service.DeletePatient(patient);
        await DisplayPatients();
    }

    [RelayCommand]
    public async Task AddNew()
    {
        await Shell.Current.Navigation.PushAsync(new PatientView(), false);
    }
}
