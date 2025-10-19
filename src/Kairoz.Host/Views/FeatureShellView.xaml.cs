using System.Windows.Controls;
using Kairoz.Host.ViewModels;

namespace Kairoz.Host.Views;

public partial class FeatureShellView : UserControl
{
    public FeatureShellView(FeatureShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
