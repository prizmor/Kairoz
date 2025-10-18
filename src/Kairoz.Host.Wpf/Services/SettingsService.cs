using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kairoz.Core.Settings;

namespace Kairoz.Host.Wpf.Services;

public sealed class SettingsService : ISettingsService
{
    private readonly SemaphoreSlim _gate = new(1, 1);
    private readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);
    private readonly string _filePath;
    private ApplicationSettings _current = ApplicationSettings.CreateDefault();
    private bool _isLoaded;

    public SettingsService()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Kairoz");
        Directory.CreateDirectory(directory);
        _filePath = Path.Combine(directory, "settings.json");
    }

    public ApplicationSettings Current => _current;

    public async ValueTask<ApplicationSettings> LoadAsync(CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (_isLoaded)
            {
                return _current;
            }

            if (File.Exists(_filePath))
            {
                await using var fileStream = File.OpenRead(_filePath);
                var loaded = await JsonSerializer.DeserializeAsync<ApplicationSettings>(fileStream, _serializerOptions, cancellationToken).ConfigureAwait(false);
                if (loaded is not null)
                {
                    _current = loaded;
                }
            }

            _isLoaded = true;
            return _current;
        }
        finally
        {
            _gate.Release();
        }
    }

    public async ValueTask SaveAsync(ApplicationSettings settings, CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            await using var fileStream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(fileStream, settings, _serializerOptions, cancellationToken).ConfigureAwait(false);
            _current = settings;
            _isLoaded = true;
        }
        finally
        {
            _gate.Release();
        }
    }
}
