using Microsoft.Extensions.DependencyInjection;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Presentation;

// Inject Dependencies
var provider = DependencyInjector.CreateServiceProvider();

// Initialize Bot Events
var weatherBotPublisher = provider.GetRequiredService<IBotPublishingService>();
var weatherDataNotifier = provider.GetRequiredService<IDataChangeNotifier<WeatherData>>();
var weatherEventManager = provider.GetRequiredService<IBotEventManager<WeatherData>>();
weatherEventManager.Attach(weatherDataNotifier, weatherBotPublisher);

// Start
new UserController(provider).Start();