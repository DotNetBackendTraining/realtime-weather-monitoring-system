using Microsoft.Extensions.DependencyInjection;
using RealTimeWeatherMonitoringApp.Application.Interfaces;

namespace RealTimeWeatherMonitoringApp.Presentation;

public class UserController
{
    private readonly IServiceProvider _provider;
    public UserController(IServiceProvider provider) => _provider = provider;

    private IWeatherDataProcessor WeatherDataProcessor => _provider.GetRequiredService<IWeatherDataProcessor>();
    private IBotNotificationService BotNotificationService => _provider.GetRequiredService<IBotNotificationService>();

    public void Start()
    {
        BotNotificationService.OnBotNotification += (_, args) => Console.WriteLine(args.Message);

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