using Kairoz.Core.Presentation;
using Kairoz.Core.Settings;

namespace Plugins.DemoFeature.ViewModels;

public sealed class DemoFeatureViewModel : ObservableObject
{
    private string _status;

    public DemoFeatureViewModel(ISettingsService settingsService)
    {
        var settings = settingsService.Current;
        _status = $"Demo plugin active at {(int)settings.CaptureFps} FPS (scale {settings.UiScale:0.##}).";
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }
}
