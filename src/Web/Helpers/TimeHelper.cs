namespace F1.Web.Helpers;

public static class TimeHelper
{
    public static string ToDisplayDate(DateOnly date, TimeOnly? time) =>
        time.HasValue
            ? TimeZoneInfo
                .ConvertTimeFromUtc(new DateTime(date, time.Value, DateTimeKind.Utc), TimeZoneInfo.Local)
                .ToString("yyyy-MM-dd HH:mm")
            : date
                .ToString("yyyy-MM-dd");
}
