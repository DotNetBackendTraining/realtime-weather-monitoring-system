using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Domain.Interfaces.Repository;

public interface IBotRepository
{
    IEnumerable<BotModel> GetAll();
}