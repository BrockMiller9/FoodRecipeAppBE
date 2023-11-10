using FoodRecipeApp;
using FoodRecipeApp.Models.Mapping;
using FoodRecipeApp.Repositories.Interface;
using FoodRecipeApp.Repositories.Repository;
using FoodRecipeApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200") // Adjust this if your Angular app is running on a different port
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<RecipeService>();
//add my service for auto mapper that i just created UserProfile
builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddHttpClient<IRecipeService, RecipeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyAllowSpecificOrigins");

app.MapControllers();

app.Run();
