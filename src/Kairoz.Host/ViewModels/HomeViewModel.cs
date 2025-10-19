using Kairoz.Core.Presentation;

namespace Kairoz.Host.ViewModels;

public sealed class HomeViewModel : ObservableObject
{
    private string _welcomeMessage = "Welcome to Kairoz";

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set => SetProperty(ref _welcomeMessage, value);
    }
}
