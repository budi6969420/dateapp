// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.DbContexts;
using DateAppApi.IServices;
using DateAppApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IDateService, DateService>();
builder.Services.AddScoped<IDateIdeaService, DateIdeaService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();