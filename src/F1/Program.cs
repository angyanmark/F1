using F1;
using F1.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddMudServices()
    .AddHttpClient<F1Service>(httpClient => httpClient.BaseAddress = new("https://api.jolpi.ca/"));

await builder.Build().RunAsync();
