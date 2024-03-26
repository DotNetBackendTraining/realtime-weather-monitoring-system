using Microsoft.Extensions.DependencyInjection;
using RealTimeWeatherMonitoringApp.Presentation.Controller;
using RealTimeWeatherMonitoringApp.Presentation.Utility;

// Inject Dependencies
var provider = DependencyInjector.CreateServiceProvider();

// Start
provider.GetRequiredService<ServerController>().Start();
await provider.GetRequiredService<UserController>().Start();