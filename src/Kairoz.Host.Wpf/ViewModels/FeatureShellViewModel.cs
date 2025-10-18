using System;
using System.Collections.Generic;
using System.Linq;
using Kairoz.Core.Presentation;
using Kairoz.Plugins;

namespace Kairoz.Host.Wpf.ViewModels;

public sealed class FeatureShellViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IReadOnlyList<IKairozPlugin> _plugins;
    private object? _activeView;
    private IKairozPlugin? _selectedPlugin;
    private bool _isInitializing;

    public FeatureShellViewModel(IEnumerable<IKairozPlugin> plugins, IServiceProvider serviceProvider)
    {
        _plugins = plugins.ToList();
        _serviceProvider = serviceProvider;
        _isInitializing = true;
        if (_plugins.Count > 0)
        {
            SelectedPlugin = _plugins[0];
        }
        _isInitializing = false;
        ActivatePlugin(SelectedPlugin);
    }

    public IReadOnlyList<IKairozPlugin> Plugins => _plugins;

    public IKairozPlugin? SelectedPlugin
    {
        get => _selectedPlugin;
        set
        {
            if (SetProperty(ref _selectedPlugin, value) && !_isInitializing)
            {
                ActivatePlugin(value);
            }
        }
    }

    public object? ActiveView
    {
        get => _activeView;
        private set => SetProperty(ref _activeView, value);
    }

    private void ActivatePlugin(IKairozPlugin? plugin)
    {
        if (plugin is null)
        {
            ActiveView = null;
            return;
        }

        ActiveView = plugin.CreateView(_serviceProvider);
    }
}
