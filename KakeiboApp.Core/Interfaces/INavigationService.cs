using System;

namespace KakeiboApp.Core.Interfaces;

public interface INavigationService
{
    Task NavigateToAsync(string route);
    Task GoBackAsync();
}
