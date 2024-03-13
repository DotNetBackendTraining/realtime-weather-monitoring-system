using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Domain.Interfaces;

public interface IBotPublishingService
{
    void Publish(BotEventArgs e);
}