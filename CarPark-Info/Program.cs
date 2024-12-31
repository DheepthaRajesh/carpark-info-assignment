using CarPark_Info.Models;
using CarPark_Info.Repositories;
using CarPark_Info.Repositories.Core;
using CarPark_Info.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Generic Repository and Unit of Work:
builder.Services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add the dbContext:
var conenctionString =  "DataSource=CarParkInfo.db";
builder.Services.AddDbContext<CarParkContext>(options =>
    options.UseSqlite(conenctionString));

// Add the ProcessCsvService:
builder.Services.AddScoped<ProcessCsvService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// To call the ProcessCsvFile() method inside ProcessCsvService once the app builds:
using (var scope = app.Services.CreateScope())
{
    var processCsvService = scope.ServiceProvider.GetRequiredService<ProcessCsvService>();
    await processCsvService.ProcessCsvFile(); 
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
