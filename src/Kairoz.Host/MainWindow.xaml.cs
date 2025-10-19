using System;
using System.Windows;
using Kairoz.Host.ViewModels;
using Kairoz.Host.Views;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

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
        RootNavigation.Navigate("0");
        NavigateTo("Home");
    }

    private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not NavigationView navigationView)
        {
            return;
        }

        if (navigationView.SelectedItem is not NavigationViewItem item)
        {
            return;
        }

        var tag = item.Tag as string;
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
            ContentFrame.Content = view;
        }
    }
}
