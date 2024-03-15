using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Evaluators;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Factory;

public class WeatherEvaluatorFactory : IEvaluatorFactory<WeatherData>
{
    public IEvaluator<WeatherData> CreateEvaluator(ConditionConfiguration config)
    {
        var value = config.Value;
        var comparison = GetComparisonOperator(config.Operator);
        return config.Type.ToLower() switch
        {
            "temperature" => new WeatherTemperatureEvaluator(value, comparison),
            "humidity" => new WeatherHumidityEvaluator(value, comparison),
            _ => throw new ArgumentException($"Unsupported condition type: {config.Type}")
        };
    }

    private Func<double, double, bool> GetComparisonOperator(string @operator)
    {
        return @operator switch
        {
            "greaterThan" => (x, y) => x > y,
            "lessThan" => (x, y) => x < y,
            _ => throw new ArgumentException($"Unsupported operator: {@operator}")
        };
    }
}