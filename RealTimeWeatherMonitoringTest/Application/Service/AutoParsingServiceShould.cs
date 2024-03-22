using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class AutoParsingServiceShould
{
    [Theory, AutoMoqData]
    public void AutoParse_NoParsingStrategyWorks_ReturnFailResult(
        string input,
        List<Mock<IParsingStrategy<TestData>>> strategyMocks,
        AutoParsingService<TestData> autoParsingService)
    {
        foreach (var mock in strategyMocks)
            autoParsingService.AddStrategy(mock.Object);

        var result = autoParsingService.AutoParse(input);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
    }

    [Theory, AutoMoqData]
    public void AutoParse_AStrategyWorks_ReturnSuccessResult(
        string input,
        TestData? resultData,
        Mock<IParsingStrategy<TestData>> strategyMock,
        AutoParsingService<TestData> autoParsingService)
    {
        autoParsingService.AddStrategy(strategyMock.Object);
        strategyMock.Setup(s => s.TryParse(input, out resultData)).Returns(true);

        var result = autoParsingService.AutoParse(input);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeSameAs(resultData);
    }

    [Theory, AutoMoqData]
    public void AutoParse_MultipleStrategiesWork_UseFirstOne(
        string input,
        Mock<IParsingStrategy<TestData>> firstStrategyMock,
        Mock<IParsingStrategy<TestData>> secondStrategyMock,
        AutoParsingService<TestData> autoParsingService)
    {
        autoParsingService.AddStrategy(firstStrategyMock.Object);
        autoParsingService.AddStrategy(secondStrategyMock.Object);

        TestData? dummy = null;
        firstStrategyMock.Setup(s => s.TryParse(input, out dummy)).Returns(true);
        secondStrategyMock.Setup(s => s.TryParse(input, out dummy)).Returns(true);

        autoParsingService.AutoParse(input);

        firstStrategyMock.Verify(s => s.TryParse(input, out dummy), Times.Once);
        secondStrategyMock.Verify(s => s.TryParse(input, out dummy), Times.Never);
    }
}