namespace F1.Helpers;

public static class TimeHelper
{
    public static string ToDisplayDate(DateOnly date, TimeOnly? time) =>
        time.HasValue
            ? new DateTime(date, time.Value).ToLocalTime().ToString("yyyy-MM-dd HH:mm")
            : date.ToDateTime(TimeOnly.MinValue).ToLocalTime().ToString("yyyy-MM-dd");
}
