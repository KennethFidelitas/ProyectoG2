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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
});

builder.Services.AddScoped<ISinpeRepository, SinpeRepository>();
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<IComercioRepository, ComercioRepository>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddHttpClient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ReporteBusiness>();
builder.Services.AddScoped<SinpeBusiness>();
builder.Services.AddScoped<CajaBusiness>();
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<UsuarioBusiness>();

builder.Services.AddScoped<AuthApiService>();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();