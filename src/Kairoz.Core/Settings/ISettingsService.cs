using System.Threading;
using System.Threading.Tasks;

namespace Kairoz.Core.Settings;

public interface ISettingsService
{
    ApplicationSettings Current { get; }

    ValueTask<ApplicationSettings> LoadAsync(CancellationToken cancellationToken = default);

    ValueTask SaveAsync(ApplicationSettings settings, CancellationToken cancellationToken = default);
}
