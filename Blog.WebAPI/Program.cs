using Blog.DAL;
using Blog.DAL.ByEF;
using Blog.DataEntities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApplication15.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// setup DbContext services
builder.Services.AddDbContext<StoreDbContext>(
    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"), 
    ServiceLifetime.Scoped);
builder.Services.AddScoped<DbContext, StoreDbContext>();

//setup CategoryEntity repository service
builder.Services.AddTransient<EFRepository<CategoryEntity>>();
builder.Services.AddTransient<IRepository<CategoryEntity>, EFRepository<CategoryEntity>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
