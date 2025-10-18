using System;

namespace Kairoz.Plugins;

public interface IKairozPlugin
{
    string Title { get; }

    object CreateView(IServiceProvider serviceProvider);
}
