using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

try
{
    // ? Habilita MVC (no solo Razor Pages)
    builder.Services.AddControllersWithViews();

    // ? Configura HttpClient para llamar a la API
    builder.Services.AddHttpClient("PersonasApi", client =>
    {
        client.BaseAddress = new Uri("https://localhost:7122/");
    });

    var app = builder.Build();

    // Middleware básico
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthorization();

    // ? Configura rutas MVC (no Razor Pages)
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Personas}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("ERROR CRÍTICO AL INICIAR:");
    Console.WriteLine(ex.ToString());
    Environment.Exit(1); // Sale con error
}
