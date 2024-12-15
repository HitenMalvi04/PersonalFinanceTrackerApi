using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerAPI.Data;

var builder = WebApplication.CreateBuilder(args);



// Add the TransactionRepository as a service
builder.Services.AddScoped<TransactionRepository>(provider =>
    new TransactionRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your UserRepository with DI container
builder.Services.AddScoped<UserRepository>(provider =>
    new UserRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
