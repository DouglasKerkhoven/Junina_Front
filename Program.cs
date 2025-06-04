using curitibano.blazor.junino.Components;
using curitibano.blazor.junino.Service;

namespace curitibano.blazor.junino;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        var apiUrl = builder.Configuration["URL_JUNINA_API"]
             ?? Environment.GetEnvironmentVariable("URL_JUNINA_API/")
             ?? "http://localhost:5000";

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(apiUrl)
        });


        builder.Services.AddScoped<ItemService>(); 
        builder.Services.AddScoped<VendasService>();


        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("https://localhost:7000") // URL da app Blazor
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });




        var app = builder.Build();

        app.UseCors();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
