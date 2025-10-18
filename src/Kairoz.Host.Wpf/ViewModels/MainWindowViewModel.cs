using Kairoz.Core.Presentation;

namespace Kairoz.Host.Wpf.ViewModels;

public sealed class MainWindowViewModel : ObservableObject
{
    private string _title = "Kairoz";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
