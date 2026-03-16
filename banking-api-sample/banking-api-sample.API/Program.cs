using System;
using System.Reflection;
using AutoMapper;
using BankingApiSample.Application.Interfaces;
using BankingApiSample.Application.Mappings;
using BankingApiSample.Application.Services;
using BankingApiSample.Domain.Interfaces;
using BankingApiSample.Infrastructure.Persistence;
using BankingApiSample.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services
builder.Services.AddControllers().AddJsonOptions(opts => {
    opts.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
// Register validators from Application assembly
builder.Services.AddValidatorsFromAssembly(typeof(BankingApiSample.Application.Validators.CreateProposalValidator).Assembly);

// DbContext
var connection = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");
builder.Services.AddDbContext<BankingDbContext>(options => options.UseSqlServer(connection));

// DI registrations
builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
builder.Services.AddScoped<IProposalService, ProposalService>();

var app = builder.Build();

// Migrate & Seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BankingDbContext>();
    db.Database.Migrate();
    await SeedData.EnsureSeedAsync(db);
}

// Configure middleware
app.UseMiddleware<Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }
