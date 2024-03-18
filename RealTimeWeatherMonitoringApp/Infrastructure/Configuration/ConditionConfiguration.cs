using RealTimeWeatherMonitoringApp.Infrastructure.Configuration.Condition;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

public record ConditionConfiguration(
    ConditionType ConditionType,
    ConditionOperator Operator,
    double Value);