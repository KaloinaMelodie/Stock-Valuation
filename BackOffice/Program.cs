using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BackOffice.Data;
using BackOffice.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Rotativa.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBContext") ?? throw new InvalidOperationException("Connection string 'AppDBContext' not found.")));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    //(options => options.SignIn.RequireConfirmedAccount = true)
    ).AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();


var app = builder.Build();



// if (args.Length == 1 && args[0].ToLower() == "seeddata")
// {
//    await Seed.SeedUsersAndRolesAsync(app);
//    Seed.SeedData(app);
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


// Configure Rotativa
//var rotativaPath = Path.Combine(app.Environment.ContentRootPath, "Rotativa");
//var rotativaPath = Path.Combine("H:\\main\\wkhtmltopdf\\bin","");
string rotativaPath = @"H:\main\wkhtmltopdf\bin";
// RotativaConfiguration.Setup(rotativaPath);
var env = app.Services.GetRequiredService<IWebHostEnvironment>();
RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");


app.Run();
