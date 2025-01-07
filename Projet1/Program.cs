using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projet1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<Projet1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Projet1Context") ?? throw new InvalidOperationException("Connection string 'Projet1Context' not found.")));

// Add session services
builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Ensure session is essential for the application
});

// Add controllers and views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

// Add session middleware
app.UseSession();

app.UseRouting();

app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
