namespace RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

public record ConditionConfiguration(
    string Type,
    string Operator,
    double Value);