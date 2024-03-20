namespace RealTimeWeatherMonitoringTest.Domain.Models;

public class TestData
{
    public int Field1 { get; }
    public string Field2 { get; }

    public TestData(int field1, string field2)
    {
        Field1 = field1;
        Field2 = field2;
    }
}