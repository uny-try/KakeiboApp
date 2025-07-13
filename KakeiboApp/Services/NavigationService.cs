using System;
using Microsoft.Maui.Controls;

using KakeiboApp.Core.Interfaces;

namespace KakeiboApp.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
