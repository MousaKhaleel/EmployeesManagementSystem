using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Infrastructure.Data;
using EmployeesManagementSystem.Infrastructure.Repositories;
using EmployeesManagementSystem.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<Employee, IdentityRole>(
	options =>
	{
		//temp for dev
		options.Password.RequiredUniqueChars = 0;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = false;
		options.Password.RequiredLength = 8;
		options.Password.RequireNonAlphanumeric = false;
	})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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
