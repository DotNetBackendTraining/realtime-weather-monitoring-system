namespace RealTimeWeatherMonitoringApp.Domain.Models;

public class WeatherData
{
    public string Location { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }

    public WeatherData(string location, float temperature, float humidity)
    {
        Location = location;
        Temperature = temperature;
        Humidity = humidity;
    }

    public override string ToString() =>
        $"WeatherData - Location: {Location}, Temperature: {Temperature}, Humidity: {Humidity}";
}