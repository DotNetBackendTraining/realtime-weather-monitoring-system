namespace RealTimeWeatherMonitoringApp.Domain.Common;

public class BotEventArgs : EventArgs
{
    public string Message { get; }
    public BotEventArgs(string message) => Message = message;
}