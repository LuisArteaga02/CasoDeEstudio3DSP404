using BibliotecaMetropoli.Data;
using BibliotecaMetropoli.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

// 2️⃣ Configurar la conexión a la base de datos
builder.Services.AddDbContext<BibliotecaMetropoliContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionBiblioteca")));

var app = builder.Build();

// 3️⃣ Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4️⃣ Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


