using MauiAppIsaac.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Services;

public class DialogService : IDialogService
{
    Task<string> IDialogService.ShowActionAsync(string title, string message, string destruction, params string[] buttons)
    {
        return Application.Current.MainPage.DisplayActionSheet(title, message, destruction, buttons);
    }

    Task<bool> IDialogService.ShowAlertAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }

    Task<bool> IDialogService.ShowConfirmationAsync(string title, string message)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, "ok", "no");
    }
}
