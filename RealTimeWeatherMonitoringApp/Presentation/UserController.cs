using Microsoft.Extensions.DependencyInjection;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Presentation;

public class UserController
{
    private readonly IServiceProvider _provider;
    public UserController(IServiceProvider provider) => _provider = provider;

    private IBotNotificationService BotNotificationService => _provider.GetRequiredService<IBotNotificationService>();

    private IDataProcessingService<WeatherData> WeatherDataProcessor =>
        _provider.GetRequiredService<IDataProcessingService<WeatherData>>();

    public void Start()
    {
        BotNotificationService.OnBotNotification += (_, args) =>
            Console.WriteLine($"\n{args.BotName}:  {args.Message}");

        while (true)
        {
            Console.WriteLine("\nEnter weather data, or (q) to quit:");

            var input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase)) break;

            var result = WeatherDataProcessor.Process(input);
            if (result.Fail) Console.WriteLine(result.Message);
        }
    }
}