using System.Text.Json;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Repository;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Repository;

public class BotRepository : IBotRepository
{
    private const string ConfigurationFilePath = "Infrastructure/configuration.json";

    private static readonly Lazy<IEnumerable<BotModel>> BotModelCollection = new(() =>
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(baseDirectory, ConfigurationFilePath);
        var jsonText = File.ReadAllText(fullPath);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var botDict = JsonSerializer.Deserialize<Dictionary<string, BotModel>>(jsonText, options);
        if (botDict == null) return Enumerable.Empty<BotModel>();

        foreach (var kvp in botDict) kvp.Value.Name = kvp.Key;
        return botDict.Values;
    });

    public IEnumerable<BotModel> GetAll() => BotModelCollection.Value;
}