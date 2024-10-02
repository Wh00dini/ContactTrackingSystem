using AutoMapper;
using ContactTrackingSystem;
using ContactTrackingSystem.Context;
using ContactTrackingSystem.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ContactTrackingSystem.Data;
using Microsoft.AspNetCore.Identity;
using ContactTrackingSystem.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ContactTrackingSystemContextConnection") ?? throw new InvalidOperationException("Connection string 'ContactTrackingSystemContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));
builder.Services.AddDbContext<ContactTrackingSystemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContactTrackingSystemContextConnection")));

builder.Services
    .AddDefaultIdentity<ContactTrackingSystemUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContactTrackingSystemContext>();

builder.Services.AddScoped<IContactRepository,ContactRepository>();
builder.Services.AddAutoMapper(typeof(ContactProfile));


var app = builder.Build();

// Seed the database when the application starts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        // Handle exceptions
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}




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
app.MapRazorPages();
app.Run();
