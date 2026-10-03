using IoTTelemetryLab.Data;
using IoTTelemetryLab.Models;
using IoTTelemetryLab.Processing;

const string deviceId = "sensor-04";
const SensorType sensorType = SensorType.CurrentIntensity;

// Генеруємо "сирі" дані з апаратними помилками
var rawData = TelemetryGenerator.GenerateRawBatch(deviceId, count: 50, minValue: -1.0, maxValue: 18.0);
Console.WriteLine($"Отримано показiв: {rawData.Count}");

// Базова частина: очищення
var cleaned = TelemetryCleaner.RemoveInvalid(rawData).ToList();
Console.WriteLine($"Пiсля очищення: {cleaned.Count}");

// Варіативна частина: оцінка станів через Pattern Matching
int criticalCount = TelemetryAnalytics.CountByStatus(cleaned, sensorType, SafetyStatus.Critical);
int warningCount = TelemetryAnalytics.CountByStatus(cleaned, sensorType, SafetyStatus.Warning);
Console.WriteLine($"Критичних показiв: {criticalCount}, попереджень: {warningCount}");

// Ковзне середнє
var averages = TelemetryAnalytics.MovingAverage(cleaned, windowSize: 5).ToList();
Console.WriteLine($"Ковзне середнє (першi 3 вiкна): " +
    string.Join(", ", averages.Take(3).Select(a => a.ToString("F2"))));

// Виклики індивідуального аналітичного методу (Варіант 4: Мін/Макс)
var (minVal, maxVal) = TelemetryAnalytics.GetMinMaxReadings(cleaned);
if (minVal.HasValue && maxVal.HasValue)
{
    Console.WriteLine($"Мiнiмальне значення сили струму: {minVal.Value:F2} А");
    Console.WriteLine($"Максимальне значення сили струму: {maxVal.Value:F2} А");
}
else
{
    Console.WriteLine("Не вдалося обчислити мiн/макс (вiдсутнi валиднi данi).");
}