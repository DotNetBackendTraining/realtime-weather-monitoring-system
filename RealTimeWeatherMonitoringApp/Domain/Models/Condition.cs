namespace RealTimeWeatherMonitoringApp.Domain.Models;

public class Condition
{
    public string Type { get; set; }
    public string Operator { get; set; }
    public double Value { get; set; }

    public Condition(string type, string @operator, double value)
    {
        Type = type;
        Operator = @operator;
        Value = value;
    }

    public override string ToString() => $"{Type} {Operator} {Value}";
}