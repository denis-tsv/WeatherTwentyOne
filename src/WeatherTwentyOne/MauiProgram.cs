﻿using Microsoft.Maui.LifecycleEvents;
using WeatherClient2021;
using WeatherTwentyOne.Mvvm;
using WeatherTwentyOne.Services;
using WeatherTwentyOne.ViewModels;
using WeatherTwentyOne.ViewModels.Widget;
using WeatherTwentyOne.Views;

namespace WeatherTwentyOne;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            });
        builder.ConfigureLifecycleEvents(lifecycle => {
#if WINDOWS
            //lifecycle
            //    .AddWindows(windows => windows.OnLaunched((app, args) => {
            //        var winuiApp = (Microsoft.UI.Xaml.Window)MauiWinUIApplication.Current.Application.Windows[0].Handler!.NativeView!;
            //        winuiApp.SetIcon("Platforms/Windows/trayicon.ico");
            //    }));
#endif
        });

        var services = builder.Services;

        services.AddSingleton<IForecastService, ForecastService>();
        services.AddSingleton<IWeatherService, WeatherService>();

        services.AddSingleton<IWidgetViewModelFactory, WidgetViewModelFactory>();
        
        services.AddTransient<FavoritesPageViewModel>();
        services.AddTransient<HomePageViewModel>();
        services.AddTransient<SettingsPageViewModel>();

        services.AddTransient<WeekForecastsWidgetViewModel>();
        services.AddTransient<DayForecastsWidgetViewModel>();

        var viewModelMapper = new ViewModelMapper();

        viewModelMapper.Register<Next24HrWidget, DayForecastsWidgetViewModel>();
        viewModelMapper.Register<Next7DWidget, WeekForecastsWidgetViewModel>();

        services.AddSingleton<IViewModelMapper>(viewModelMapper);

#if WINDOWS
            //services.AddSingleton<ITrayService, WinUI.TrayService>();
            //services.AddSingleton<INotificationService, WinUI.NotificationService>();
#elif MACCATALYST
            //services.AddSingleton<ITrayService, MacCatalyst.TrayService>();
            //services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
#endif




        return builder.Build();
    }
}