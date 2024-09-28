using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PruebaCodere.client;
using PruebaCodere.client.Services;
using CurrieTechnologies.Razor.SweetAlert2;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5078") });
builder.Services.AddScoped<GrupoIDService, GrupoService>(); // Correcto
builder.Services.AddScoped<EmpleadosIDService, EmpleadoService>(); // Correcto


builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
