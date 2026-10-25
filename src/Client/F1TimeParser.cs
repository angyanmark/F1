using System.Globalization;

namespace F1.Client;

internal static class F1TimeParser
{
    private const string Utc = "HH:mm:ss'Z'";

    public static TimeOnly Parse(string time) =>
        TimeOnly.ParseExact(time, Utc, CultureInfo.InvariantCulture);
}
