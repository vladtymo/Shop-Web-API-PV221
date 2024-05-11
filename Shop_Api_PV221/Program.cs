using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using Hangfire;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Shop_Api_PV221;
using Shop_Api_PV221.Helpers;
using Shop_Api_PV221.Services;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("MainDb")!;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// TODO: configure swagger with JWT 
builder.Services.AddSwaggerGen();
builder.Services.AddJWT(builder.Configuration);
builder.Services.AddRequirements();

builder.Services.AddDbContext(connStr);
builder.Services.AddIdentity();
builder.Services.AddRepositories();

builder.Services.AddAutoMapper();
builder.Services.AddFluentValidators();

builder.Services.AddCustomServices();
builder.Services.AddScoped<ICartService, CartService>();
//builder.Services.AddScoped<IViewRender, ViewRender>();

// hangfire
builder.Services.AddHangfire(connStr);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.SeedRoles().Wait();
    scope.ServiceProvider.SeedAdmin().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<GlobalErrorHandler>();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200", "http://localhost:3000", "https://yellow-pond-03cd86610.5.azurestaticapps.net")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthorization();

app.UseHangfireDashboard("/dash");
JobConfigurator.AddJobs();

app.MapControllers();

app.Run();
