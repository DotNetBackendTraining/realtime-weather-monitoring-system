using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class AutoParsingServiceShould
{
    private readonly AutoParsingService<TestData> _autoParsingService;

    private readonly List<Mock<IParsingStrategy<TestData>>> _mockStrategies =
    [
        new Mock<IParsingStrategy<TestData>>(),
        new Mock<IParsingStrategy<TestData>>()
    ];

    private readonly List<string> _inputs =
    [
        "{ 'text': 'This can be parsed only by Strategy1' }",
        "<text>This can be parsed only by Strategy2</text>"
    ];

    private const string InvalidInput = "This cannot be parsed by anything";

    private TestData? _tryParseResult;

    public AutoParsingServiceShould()
    {
        _autoParsingService = new AutoParsingService<TestData>();
        _mockStrategies.ForEach(s => _autoParsingService.AddStrategy(s.Object));

        for (var i = 0; i < _mockStrategies.Count; i++)
        {
            var i1 = i;
            _mockStrategies[i]
                .Setup(s => s.TryParse(_inputs[i1], out _tryParseResult))
                .Returns(true);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void AutoParse_UsesFirstSuccessfulStrategy(int inputsIndex)
    {
        var result = _autoParsingService.AutoParse(_inputs[inputsIndex]);
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();

        _mockStrategies[inputsIndex].Verify(s => s.TryParse(_inputs[inputsIndex], out _tryParseResult), Times.Once);
        _mockStrategies[inputsIndex].VerifyNoOtherCalls();
    }

    [Fact]
    public void AutoParse_ReturnsFailureWhenNoStrategiesSucceed()
    {
        var result = _autoParsingService.AutoParse(InvalidInput);
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();

        _mockStrategies.ForEach(strategy =>
            strategy.Verify(s =>
                s.TryParse(It.IsAny<string>(), out _tryParseResult), Times.Once));
    }
}