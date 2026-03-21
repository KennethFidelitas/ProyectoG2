using MiProyectoMVC.Repositories;
using MiProyectoMVC.Business;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();



builder.Services.AddScoped<ISinpeRepository, SinpeRepository>();
builder.Services.AddScoped<IComercioRepository, ComercioRepository>();
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();





builder.Services.AddScoped<SinpeBusiness>();
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<CajaBusiness>();

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}")
.WithStaticAssets();

app.Run();
