using Microsoft.Extensions.DependencyInjection;

namespace RealTimeWeatherMonitoringApp.Presentation;

public static class DependencyInjector
{
    public static IServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        Inject(services);
        return services.BuildServiceProvider();
    }

    private static void Inject(ServiceCollection services)
    {
    }
}