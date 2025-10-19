using System.Windows.Controls;
using Kairoz.Host.ViewModels;

namespace Kairoz.Host.Views;

public partial class HomeView : UserControl
{
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
