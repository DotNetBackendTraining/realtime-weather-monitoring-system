namespace RealTimeWeatherMonitoringApp.Domain.Models;

public class BotModel
{
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public string Message { get; set; }
    public List<Condition> Conditions { get; init; } = [];

    public BotModel(string name, bool enabled, string message)
    {
        Name = name;
        Enabled = enabled;
        Message = message;
    }

    public override string ToString()
    {
        var conditions = string.Join(", ", Conditions.Select(c => c.ToString()));
        return $"BotModel[Name={Name}, Enabled={Enabled}, Message=\"{Message}\", Conditions=[{conditions}]]";
    }
}