using Kairoz.Core.Settings;
using Kairoz.Host.Wpf.Services;
using Kairoz.Host.Wpf.ViewModels;
using Kairoz.Host.Wpf.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plugins.DemoFeature;
using Plugins.DemoFeature.ViewModels;
using Plugins.DemoFeature.Views;
using System;
using System.Windows;
using Wpf.Ui.Appearance;

namespace Kairoz.Host.Wpf;

public partial class App : Application
{
    private IHost? _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(static (context, configuration) =>
            {
                configuration.SetBasePath(AppContext.BaseDirectory);
            })
            .ConfigureServices(ConfigureServices)
            .Build();

        ApplicationThemeManager.Apply(ApplicationTheme.Dark, AccentColor.Violet);

        await _host.StartAsync();

        var settingsService = _host.Services.GetRequiredService<ISettingsService>();
        await settingsService.LoadAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<HomeView>();
        services.AddSingleton<HomeViewModel>();

        services.AddSingleton<SettingsView>();
        services.AddSingleton<SettingsViewModel>();

        services.AddSingleton<FeatureShellView>();
        services.AddSingleton<FeatureShellViewModel>();

        services.AddSingleton<DemoFeatureView>();
        services.AddSingleton<DemoFeatureViewModel>();
        services.AddSingleton<IKairozPlugin, DemoFeaturePlugin>();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        base.OnExit(e);
    }
}
