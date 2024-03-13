namespace RealTimeWeatherMonitoringApp.Domain.Common;

public class BotEventArgs : EventArgs
{
    public string Message { get; set; }
    public BotEventArgs(string message) => Message = message;
}