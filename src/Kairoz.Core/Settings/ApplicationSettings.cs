namespace Kairoz.Core.Settings;

public sealed class ApplicationSettings
{
    public CaptureFpsPreset CaptureFps { get; set; } = CaptureFpsPreset.Fps30;

    public double UiScale { get; set; } = 1.0;

    public static ApplicationSettings CreateDefault()
    {
        return new ApplicationSettings();
    }
}
