using Microsoft.AspNetCore.Authentication.Cookies;
using MiProyectoMVC.Business;
using MiProyectoMVC.Repositories;
using MiProyectoMVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// Cookie Auth
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

// HTTP Client para la API externa
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
});

// Repositorios
builder.Services.AddScoped<ISinpeRepository, SinpeRepository>();
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<IComercioRepository, ComercioRepository>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddHttpClient<IUsuarioRepository, UsuarioRepository>();

// Business
builder.Services.AddScoped<ReporteBusiness>();
builder.Services.AddScoped<SinpeBusiness>();
builder.Services.AddScoped<CajaBusiness>();
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<UsuarioBusiness>();
<<<<<<< Updated upstream
var app = builder.Build();
=======
>>>>>>> Stashed changes

// Auth Service
builder.Services.AddSingleton<AuthFileService>();

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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();