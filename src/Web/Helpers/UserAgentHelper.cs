using System.Reflection;

namespace F1.Web.Helpers;

public static class UserAgentHelper
{
    public static string? GetUserAgent()
    {
        var entryAssemblyName = Assembly.GetEntryAssembly()?.GetName();
        var appName = entryAssemblyName?.Name;
        var appVersion = entryAssemblyName?.Version?.ToString();

        return appName is not null && appVersion is not null
            ? $"{appName}/{appVersion}"
            : null;
    }
}
