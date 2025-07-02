using System;
using Microsoft.Maui.Controls;

using KakeiboApp.Core.Interfaces;

namespace KakeiboApp.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
    {
        await Shell.Current.GoToAsync(route, parameters ?? new Dictionary<string, object>());
    }

    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
