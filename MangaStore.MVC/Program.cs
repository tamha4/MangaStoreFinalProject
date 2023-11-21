using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MangaStore.Service.GenreTypeService;
using MangaStore.Service.MangaS;
using MangaStore.Service.StoreService;
using Microsoft.EntityFrameworkCore;
using MangaStore.Data;
using MangaStore.Service.ImageService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MangaStoreDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMangaService, MangaService>();
builder.Services.AddScoped<IGenreTypeService, GenreTypeService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Register the hosting environment

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
