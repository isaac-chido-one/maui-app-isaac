using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppIsaac.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.ViewModels;

public partial class TestViewModel : ObservableObject
{
    [ObservableProperty]
    string text = string.Empty;

    [ObservableProperty]
    int count = 0;

    private readonly IFunctions _functions;

    public TestViewModel()
    {
        this._functions = App.Current.Services.GetRequiredService<IFunctions>();
    }

    [RelayCommand]
    public void CambiarTexto()
    {
        Count++;

        if (this._functions != null)
        {
            Text = this._functions.CambiarTexto("Hola gente", Count);
        }
    }
}
