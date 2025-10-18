using System.Windows.Controls;
using Kairoz.Host.Wpf.ViewModels;

namespace Kairoz.Host.Wpf.Views;

public partial class HomeView : UserControl
{
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
