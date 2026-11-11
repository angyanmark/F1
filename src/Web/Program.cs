using F1.Client;
using F1.Web;
using F1.Web.Helpers;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var userAgent = UserAgentHelper.GetUserAgent();

builder.Services
    .AddMudServices()
    .AddHttpClient<F1HttpClient>(httpClient =>
    {
        httpClient.BaseAddress = new(F1HttpClient.BaseAddress);
        if (userAgent is not null)
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
        }
    });

await builder.Build().RunAsync();
