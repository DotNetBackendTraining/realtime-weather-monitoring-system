namespace RealTimeWeatherMonitoringApp.Domain.Models;

public class BotModel
{
    public bool Enabled { get; set; }
    public string Message { get; set; }
    public List<Condition> Conditions { get; init; } = [];

    public BotModel(bool enabled, string message)
    {
        Enabled = enabled;
        Message = message;
    }

    public override string ToString()
    {
        var conditions = string.Join(", ", Conditions.Select(c => c.ToString()));
        return $"BotModel[Enabled={Enabled}, Message=\"{Message}\", Conditions=[{conditions}]]";
    }
}