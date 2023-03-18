using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CVManagerRazor.Data;
using CVManagerRazor.Models;
using CVManagerRazor.FileUploadService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<CVManagerRazorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CVManagerRazorContext") 
    ?? throw new InvalidOperationException("Connection string 'CVManagerRazorContext' not found.")));

builder.Services.AddMvc().AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Entries/Index", ""));
builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
