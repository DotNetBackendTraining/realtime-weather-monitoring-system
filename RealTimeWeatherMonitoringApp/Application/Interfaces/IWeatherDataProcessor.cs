using RealTimeWeatherMonitoringApp.Application.Common;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces;

public interface IWeatherDataProcessor
{
    /// <summary>
    /// Parses the provided input string to extract weather data and passes the result through the system
    /// with the newly parsed weather data.
    /// </summary>
    /// <param name="input">The raw input string containing weather data to be parsed.</param>
    /// <returns>A <see cref="ParsingResult{WeatherData}"/> object that encapsulates the result of the parsing operation,
    /// indicating success or failure, and containing the parsed <see cref="WeatherData"/> if successful.</returns>
    ParsingResult<WeatherData> Process(string input);
}