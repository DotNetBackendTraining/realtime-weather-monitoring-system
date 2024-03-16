using RealTimeWeatherMonitoringApp.Application.Common;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

namespace RealTimeWeatherMonitoringApp.Application.Service;

public class DataProcessingService<TData> : IDataProcessingService<TData>
{
    private readonly IAutoParsingService<TData> _autoParsingService;
    private readonly IDataReceiver<TData> _receiver;

    public DataProcessingService(IAutoParsingService<TData> autoParsingService, IDataReceiver<TData> receiver)
    {
        _autoParsingService = autoParsingService;
        _receiver = receiver;
    }

    public ParsingResult<TData> Process(string input)
    {
        var result = _autoParsingService.AutoParse(input);
        if (result is { Success: true, Data: not null })
            _receiver.Receive(result.Data);
        return result;
    }
}