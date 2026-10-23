using F1.Client;
using F1.Web;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddMudServices()
    .AddHttpClient<F1HttpClient>(httpClient =>
    {
        httpClient.BaseAddress = new("https://api.jolpi.ca/");
        if (AppInfo.UserAgent is not null)
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AppInfo.UserAgent);
        }
    });

await builder.Build().RunAsync();
