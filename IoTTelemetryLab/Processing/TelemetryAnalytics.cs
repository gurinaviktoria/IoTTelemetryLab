// Processing/TelemetryAnalytics.cs
using IoTTelemetryLab.Models;

namespace IoTTelemetryLab.Processing;

//містить методи для аналізу та агрегації телеметричних даних
public static class TelemetryAnalytics
{
    //підраховує кількість показів із заданим станом безпеки
    public static int CountByStatus(
        IEnumerable<TelemetryReading> readings,
        SensorType type,
        SafetyStatus target)
    {
        return readings.Count(r => StatusEvaluator.Evaluate(type, r.Value) == target);
    }

    // Ковзне середнє за вікном N показів — типова аналітична задача для телеметрії
    public static IEnumerable<double> MovingAverage(
        IEnumerable<TelemetryReading> readings, int windowSize)
    {
        var values = readings.Select(r => r.Value).ToList();

        for (int i = 0; i <= values.Count - windowSize; i++)
        {
            yield return values.Skip(i).Take(windowSize).Average();
        }
    }

    //знаходить мінімальне та максимальне значення
    public static (double? Min, double? Max) GetMinMaxReadings(IEnumerable<TelemetryReading> readings)
    {
        var list = readings.ToList();
        if (!list.Any())
            return (null, null);

        double min = list.Select(r => r.Value).Min();
        double max = list.Select(r => r.Value).Max();

        return (min, max);
    }
}