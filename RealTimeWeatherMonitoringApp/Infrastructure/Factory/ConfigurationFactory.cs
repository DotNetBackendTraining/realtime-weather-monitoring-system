using System.Text.Json;
using System.Text.Json.Serialization;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Factory;

public class ConfigurationFactory : IConfigurationFactory
{
    private static readonly Lazy<JsonSerializerOptions> JsonOptions = new(() => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    });

    private readonly Lazy<IEnumerable<BotConfiguration>> _botConfigurations;

    public ConfigurationFactory(string configurationFilepath)
    {
        _botConfigurations = new Lazy<IEnumerable<BotConfiguration>>(() =>
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDirectory, configurationFilepath);
            var jsonText = File.ReadAllText(fullPath);

            var botDict = JsonSerializer.Deserialize<Dictionary<string, BotConfiguration>>(jsonText, JsonOptions.Value);
            return botDict == null
                ? Enumerable.Empty<BotConfiguration>()
                : botDict.Select(kvp => kvp.Value with { Name = kvp.Key });
        });
    }

    public IEnumerable<BotConfiguration> CreateBotConfigurations() => _botConfigurations.Value;
}