using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

public interface IBotNotificationService
{
    event EventHandler<BotEventArgs> OnBotNotification;
}