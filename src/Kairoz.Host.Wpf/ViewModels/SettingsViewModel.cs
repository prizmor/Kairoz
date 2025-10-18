using System;
using System.Threading.Tasks;
using Kairoz.Core.Presentation;
using Kairoz.Core.Settings;

namespace Kairoz.Host.Wpf.ViewModels;

public sealed class SettingsViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private bool _isLoaded;
    private CaptureFpsPreset _selectedCaptureFps;
    private double _uiScale;

    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        SaveCommand = new AsyncRelayCommand(SaveAsync, () => _isLoaded);
        CaptureFpsOptions = Enum.GetValues<CaptureFpsPreset>();
    }

    public Array CaptureFpsOptions { get; }

    public CaptureFpsPreset SelectedCaptureFps
    {
        get => _selectedCaptureFps;
        set
        {
            if (SetProperty(ref _selectedCaptureFps, value) && _isLoaded)
            {
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public double UiScale
    {
        get => _uiScale;
        set
        {
            if (SetProperty(ref _uiScale, value) && _isLoaded)
            {
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public AsyncRelayCommand SaveCommand { get; }

    public async Task LoadAsync()
    {
        var settings = await _settingsService.LoadAsync().ConfigureAwait(false);
        _isLoaded = false;
        SelectedCaptureFps = settings.CaptureFps;
        UiScale = settings.UiScale;
        _isLoaded = true;
        SaveCommand.RaiseCanExecuteChanged();
    }

    private async Task SaveAsync()
    {
        if (!_isLoaded)
        {
            return;
        }

        var updated = new ApplicationSettings
        {
            CaptureFps = SelectedCaptureFps,
            UiScale = UiScale
        };

        await _settingsService.SaveAsync(updated).ConfigureAwait(false);
    }
}
