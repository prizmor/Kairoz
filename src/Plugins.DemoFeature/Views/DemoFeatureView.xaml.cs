using System.Windows.Controls;
using Plugins.DemoFeature.ViewModels;

namespace Plugins.DemoFeature.Views;

public partial class DemoFeatureView : UserControl
{
    public DemoFeatureView(DemoFeatureViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
