namespace RealTimeWeatherMonitoringApp.Domain.Models;

public class Condition
{
    public string Type { get; set; }
    public string Operator { get; set; }
    public float Value { get; set; }

    public Condition(string type, string @operator, float value)
    {
        Type = type;
        Operator = @operator;
        Value = value;
    }

    public override string ToString() => $"{Type} {Operator} {Value}";
}