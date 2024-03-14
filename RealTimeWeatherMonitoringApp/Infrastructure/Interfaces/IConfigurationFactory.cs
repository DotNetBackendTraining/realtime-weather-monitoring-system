using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;

public interface IConfigurationFactory
{
    IEnumerable<BotConfiguration> CreateBotConfigurations();
}