using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Service;

public class BotEventDispatcher : IBotNotificationService, IBotPublishingService
{
    public event EventHandler<BotEventArgs>? OnBotNotification;

    public void Publish(BotEventArgs e) => OnBotNotification?.Invoke(this, e);
}