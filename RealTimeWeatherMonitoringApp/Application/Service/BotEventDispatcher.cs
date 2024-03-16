using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Application.Service;

public class BotEventDispatcher : IBotNotificationService, IBotPublishingService
{
    public event EventHandler<BotEventArgs>? OnBotNotification;

    public void Publish(BotEventArgs e) => OnBotNotification?.Invoke(this, e);
}