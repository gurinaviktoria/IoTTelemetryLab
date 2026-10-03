// Processing/TelemetryCleaner.cs
using IoTTelemetryLab.Models;

namespace IoTTelemetryLab.Processing;

//надає методи для фільтрації та очищення телеметричних даних
public static class TelemetryCleaner
{
    private const double ErrorMarker = -999.0;

    //видаляє недопустимі покази(Nan та некоректні апаратні маркери)
    public static IEnumerable<TelemetryReading> RemoveInvalid(
        IEnumerable<TelemetryReading> raw)
    {
        return raw.Where(r =>
            !double.IsNaN(r.Value) &&
            r.Value != ErrorMarker);
    }
}