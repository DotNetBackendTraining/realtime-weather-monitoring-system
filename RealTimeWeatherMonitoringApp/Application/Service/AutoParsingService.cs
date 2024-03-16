using RealTimeWeatherMonitoringApp.Application.Common;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

namespace RealTimeWeatherMonitoringApp.Application.Service;

public class AutoParsingService<TResult> : IAutoParsingService<TResult>
{
    private readonly List<IParsingStrategy<TResult>> _strategies = [];
    public void AddStrategy(IParsingStrategy<TResult> strategy) => _strategies.Add(strategy);
    public void RemoveStrategy(IParsingStrategy<TResult> strategy) => _strategies.Remove(strategy);

    public ParsingResult<TResult> AutoParse(string input)
    {
        foreach (var strategy in _strategies)
            if (strategy.TryParse(input, out var result))
                return new ParsingResult<TResult>(true, $"Parsing succeeded with strategy '{strategy}'")
                    { Data = result };

        return new ParsingResult<TResult>(false, $"No valid parsing strategy was found for input '{input}'");
    }
}