using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces;

public interface IBotNotificationService
{
    event EventHandler<BotEventArgs> OnBotNotification;
}