using System.Windows.Controls;
using Kairoz.Host.Wpf.ViewModels;

namespace Kairoz.Host.Wpf.Views;

public partial class FeatureShellView : UserControl
{
    public FeatureShellView(FeatureShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
