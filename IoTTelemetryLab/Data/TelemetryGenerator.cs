// Data/TelemetryGenerator.cs
using IoTTelemetryLab.Models;

namespace IoTTelemetryLab.Data;

//генератор тестових телеметричних даних
public static class TelemetryGenerator
{
    //генерує пачку показів датчика з випадковими значеннями та апаратними збоями
    public static List<TelemetryReading> GenerateRawBatch(
        string deviceId, int count, double minValue, double maxValue)
    {
        var random = new Random();
        var readings = new List<TelemetryReading>();
        var baseTime = DateTime.UtcNow;

        for (int i = 0; i < count; i++)
        {
            // ~10% показів — апаратна помилка
            double value = random.NextDouble() < 0.1
                ? (random.Next(2) == 0 ? -999.0 : double.NaN)
                : Math.Round(minValue + random.NextDouble() * (maxValue - minValue), 2);

            readings.Add(new TelemetryReading(
                DeviceId: deviceId,
                Value: value,
                Timestamp: baseTime.AddSeconds(i * 5)));
        }

        return readings;
    }
}