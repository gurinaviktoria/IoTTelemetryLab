// Models/TelemetryReading.cs
namespace IoTTelemetryLab.Models;

// readonly record struct: незмінний, легкий (value type) — ідеально
// для великої кількості короткоживучих показів з датчиків
public readonly record struct TelemetryReading(
    string DeviceId,
    double Value,
    DateTime Timestamp);