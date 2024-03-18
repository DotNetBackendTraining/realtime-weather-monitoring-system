using System.Text.Json;
using System.Text.Json.Serialization;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Factory;

public class ConfigurationFactory : IConfigurationFactory
{
    private const string BotConfigurationFilePath = "Infrastructure/Configuration/configuration.json";

    private static readonly Lazy<JsonSerializerOptions> JsonOptions = new(() => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    });

    private readonly Lazy<IEnumerable<BotConfiguration>> _botConfigurations = new(() =>
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(baseDirectory, BotConfigurationFilePath);
        var jsonText = File.ReadAllText(fullPath);

        var botDict = JsonSerializer.Deserialize<Dictionary<string, BotConfiguration>>(jsonText, JsonOptions.Value);
        return botDict == null
            ? Enumerable.Empty<BotConfiguration>()
            : botDict.Select(kvp => kvp.Value with { Name = kvp.Key });
    });

    public IEnumerable<BotConfiguration> CreateBotConfigurations() => _botConfigurations.Value;
}