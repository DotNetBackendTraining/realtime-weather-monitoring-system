using System.Text.Json;
using System.Text.Json.Serialization;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Factory;

public class ConfigurationFactory : IConfigurationFactory
{
    private const string ConfigurationFilePath = "Infrastructure/Configuration/configuration.json";

    private readonly Lazy<IEnumerable<BotConfiguration>> _botConfigurations = new(() =>
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(baseDirectory, ConfigurationFilePath);
        var jsonText = File.ReadAllText(fullPath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
        var botDict = JsonSerializer.Deserialize<Dictionary<string, BotConfiguration>>(jsonText, options);
        if (botDict == null) return Enumerable.Empty<BotConfiguration>();

        return botDict
            .Select(kvp => kvp.Value with { Name = kvp.Key });
    });

    public IEnumerable<BotConfiguration> CreateBotConfigurations() => _botConfigurations.Value;
}