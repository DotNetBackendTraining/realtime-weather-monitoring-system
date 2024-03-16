using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Application.Service;

public class MonitoringService<TData> : IDataReceiver<TData>, IDataChangeNotifier<TData>
{
    public void Receive(TData data) =>
        OnDataChange?.Invoke(this, new DataChangeEventArgs<TData>(data));

    public event EventHandler<DataChangeEventArgs<TData>>? OnDataChange;
}