using System.Reflection;

namespace F1.Web;

internal static class AppInfo
{
    private static readonly AssemblyName? entryAssemblyName = Assembly.GetEntryAssembly()?.GetName();

    public static string? AppName => entryAssemblyName?.Name;
    public static string? AppVersion => entryAssemblyName?.Version?.ToString();
    public static int? AppVersionMajor => entryAssemblyName?.Version?.Major;

    public static string? UserAgent =>
        AppName is not null && AppVersion is not null
            ? $"{AppName}/{AppVersion}"
            : null;
}
