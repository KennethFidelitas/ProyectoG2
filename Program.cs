using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiProyectoMVC.Business;
using MiProyectoMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });


builder.Services.AddAuthorization();
builder.Services.AddAuthentication();


builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
});


builder.Services.AddScoped<ISinpeRepository, SinpeRepository>();
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<IComercioRepository, ComercioRepository>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddHttpClient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddHttpClient<IConfiguracionComercioRepository, ConfiguracionComercioRepository>();


builder.Services.AddScoped<ReporteBusiness>();
builder.Services.AddScoped<SinpeBusiness>();
builder.Services.AddScoped<CajaBusiness>();
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<UsuarioBusiness>();
builder.Services.AddScoped<ConfiguracionComercioBusiness>();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cajas}/{action=Index}/{id?}");

app.Run();