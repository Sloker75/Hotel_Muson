using DLL.Context;
using Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var identityBuilder = builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<AppRole>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

BLL.Infrastructure.Configuration.ConfigurationService(builder.Services, connectionString, identityBuilder);

Muson.Infrastructure.Configuration.ConfigurationService(identityBuilder);

var app = builder.Build();


await Seed.SeedUsersAndRolesAsync(app);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
