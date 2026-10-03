// Processing/StatusEvaluator.cs
using IoTTelemetryLab.Models;

namespace IoTTelemetryLab.Processing;

//перелічує можливі рівні безпеки стану датчика
public enum SafetyStatus { Normal, Warning, Critical }

//надає класифікатор показників датчиків за допомогою паттерн-матчингу

public static class StatusEvaluator
{
    //оцінює стан показу датчика відповідно до встановлених порогів
    public static SafetyStatus Evaluate(SensorType type, double value) => type switch
    {
        SensorType.Temperature => value switch
        {
            < 0 or > 80 => SafetyStatus.Critical,
            >= 60 and <= 80 => SafetyStatus.Warning,
            _ => SafetyStatus.Normal
        },

        SensorType.Vibration => value switch
        {
            > 5.0 => SafetyStatus.Critical,
            > 2.5 => SafetyStatus.Warning,
            _ => SafetyStatus.Normal
        },

        SensorType.Voltage => value switch
        {
            < 200 or > 240 => SafetyStatus.Critical,
            < 210 or > 230 => SafetyStatus.Warning,
            _ => SafetyStatus.Normal
        },

        //по варіанту 4

        SensorType.CurrentIntensity => value switch
        {
            < 0.5 or > 15 => SafetyStatus.Critical,
            < 1 or > 12 => SafetyStatus.Warning,
            _ => SafetyStatus.Normal
        },

        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}