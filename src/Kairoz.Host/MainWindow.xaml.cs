using System;
using System.Linq;
using System.Windows;
using Kairoz.Host.ViewModels;
using Kairoz.Host.Views;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;

namespace Kairoz.Host;

public partial class MainWindow : FluentWindow
{
    private readonly IServiceProvider _serviceProvider;
    private bool _isInitialized;

    public MainWindow(MainWindowViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        DataContext = viewModel;
        _serviceProvider = serviceProvider;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            return;
        }

        _isInitialized = true;
        RootNavigation.SelectedItem = RootNavigation.MenuItems.FirstOrDefault();
        NavigateTo("Home");
    }

    private void OnNavigationItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        var tag = args.InvokedItemContainer?.Tag as string;
        NavigateTo(tag);
    }

    private void NavigateTo(string? tag)
    {
        object? view = tag switch
        {
            "Home" => _serviceProvider.GetRequiredService<HomeView>(),
            "Settings" => _serviceProvider.GetRequiredService<SettingsView>(),
            "Features" => _serviceProvider.GetRequiredService<FeatureShellView>(),
            _ => null
        };

        if (view is not null)
        {
            RootContent.Content = view;
        }
    }
}
