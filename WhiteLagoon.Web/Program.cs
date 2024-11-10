using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
#region DBContext
//Look for connection string in appsettings.json
var con = builder.Configuration.GetConnectionString("DefaultConnection"); 
builder.Services.AddDbContext<ApplicationDbContext>(option=>option.UseSqlServer(con));
#endregion
#region Repository
builder.Services.AddScoped<IVillaRepository,VillaRepository>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
