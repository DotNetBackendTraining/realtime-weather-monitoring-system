using RealTimeWeatherMonitoringApp.Presentation;

var provider = DependencyInjector.CreateServiceProvider();
new UserController(provider).Start();