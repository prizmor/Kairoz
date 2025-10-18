using System.Windows;
using System.Windows.Controls;
using Kairoz.Host.Wpf.ViewModels;

namespace Kairoz.Host.Wpf.Views;

public partial class SettingsView : UserControl
{
    private bool _isInitialized;

    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            return;
        }

        _isInitialized = true;

        if (DataContext is SettingsViewModel viewModel)
        {
            await viewModel.LoadAsync();
        }
    }
}
