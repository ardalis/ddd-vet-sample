using BlazorShared;
using FrontDesk.Blazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontDesk.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var baseUrlConfig = new BaseUrlConfiguration();
            builder.Configuration.Bind(BaseUrlConfiguration.CONFIG_NAME, baseUrlConfig);
            builder.Services.AddScoped(sp => baseUrlConfig);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<DoctorService>();

            await builder.Build().RunAsync();
        }
    }
}
