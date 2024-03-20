using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Common;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class DataProcessingServiceShould
{
    private readonly DataProcessingService<TestData> _dataProcessor;

    private readonly Mock<IAutoParsingService<TestData>> _mockAutoParsingService = new();
    private readonly Mock<IDataReceiver<TestData>> _mockReceiver = new();

    private const string InvalidInput = "This input cannot be parsed";
    private const string ValidInput = "<test>XML Data</test>";

    private readonly ParsingResult<TestData> _badResult = new(false, "Parsing failed");
    private readonly ParsingResult<TestData> _goodResult = new(true, "Parsing succeeded");

    public DataProcessingServiceShould()
    {
        _dataProcessor = new DataProcessingService<TestData>(_mockAutoParsingService.Object, _mockReceiver.Object);
        _mockAutoParsingService.Setup(s => s.AutoParse(InvalidInput)).Returns(_badResult);
        _mockAutoParsingService.Setup(s => s.AutoParse(ValidInput)).Returns(_goodResult);
    }

    [Theory]
    [InlineData(InvalidInput)]
    [InlineData(ValidInput)]
    public void Process_Always_ParseAndReturnResult(string parsingInput)
    {
        var result = _dataProcessor.Process(parsingInput);
        result.Should().NotBeNull();
        _mockAutoParsingService.Verify(s => s.AutoParse(parsingInput), Times.Once);
    }

    [Fact]
    public void Process_OnParsingFail_NotSendToReceiver()
    {
        _dataProcessor.Process(InvalidInput);
        _mockReceiver.Verify(r => r.Receive(It.IsAny<TestData>()), Times.Never);
    }

    [Fact]
    public void Process_OnSuccessfulParsing_SendResultToReceiver()
    {
        _dataProcessor.Process(ValidInput);
        _mockReceiver.Verify(r => r.Receive(It.IsAny<TestData>()), Times.Once);
    }
}