using FnBReport.GUI.Components;
using MudBlazor;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using FnBReport.DAL.Models;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Repositories;
using FnBReport.BLL.Services;
using FnBReport.GUI.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=POSReport;Integrated Security=True";
builder.Services.AddDbContext<POSReportContext>(options =>
    options.UseSqlServer(connectionString));

// DAL
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreGroupRepository, StoreGroupRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
builder.Services.AddScoped<IMonthlySaleHeaderRepository, MonthlySaleHeaderRepository>();
builder.Services.AddScoped<IMonthlySaleRepository, MonthlySaleRepository>();

// BLL
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMonthlySaleService, MonthlySaleService>();

// GUI
builder.Services.AddScoped<IStoreAppService, StoreAppService>();
builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IMonthlySaleAppService, MonthlySaleAppService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(config => {
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
