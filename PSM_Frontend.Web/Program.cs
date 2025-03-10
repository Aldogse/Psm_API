using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PSM_Frontend.Web;
using PSM_Frontend.Web.Interface;
using PSM_Frontend.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("ezpsm.azurewebsites.net") });
builder.Services.AddScoped<IDepartmentService, ProductsService>();

await builder.Build().RunAsync();
