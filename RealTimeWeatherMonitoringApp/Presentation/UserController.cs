using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Presentation;

public class UserController
{
    private readonly IBotNotificationService _botNotificationService;
    private readonly IDataProcessingService<WeatherData> _weatherDataProcessor;

    public UserController(
        IBotNotificationService botNotificationService,
        IDataProcessingService<WeatherData> weatherDataProcessor)
    {
        _botNotificationService = botNotificationService;
        _weatherDataProcessor = weatherDataProcessor;
    }

    public void Start()
    {
        _botNotificationService.OnBotNotification += (_, args) =>
            Console.WriteLine($"\n{args.BotName}:  {args.Message}");

        while (true)
        {
            Console.WriteLine("\nEnter weather data, or (q) to quit:");

            var input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase)) break;

            var result = _weatherDataProcessor.Process(input);
            if (result.Fail) Console.WriteLine(result.Message);
        }
    }
}