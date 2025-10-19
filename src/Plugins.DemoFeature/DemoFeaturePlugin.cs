using Kairoz.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Plugins.DemoFeature.Views;
using System;

namespace Plugins.DemoFeature;

public sealed class DemoFeaturePlugin : IKairozPlugin
{
    public string Title => "Demo Feature";

    public object CreateView(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<DemoFeatureView>();
    }
}
