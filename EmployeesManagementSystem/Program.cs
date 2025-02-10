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
		//temp for dev TODO: remove
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

//using (var scope = app.Services.CreateScope())
//{
//	var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//	var roles = new[] { "Admin", "Employee" };
//	foreach (var role in roles)
//	{
//		if (!await _roleManager.RoleExistsAsync(role))
//		{
//			await _roleManager.CreateAsync(new IdentityRole(role));
//		}
//	}
//}

//using (var scope = app.Services.CreateScope())
//{
//	var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
//	var name = "Admin";
//	var email = "Admin@adm.com";
//	var password = "Admin@1234";
//	if (await _userManager.FindByEmailAsync(email) == null)
//	{
//		var employeeAdmin = new Employee
//		{
//			EmployeeName = name,

//		};
//		await _userManager.CreateAsync(employeeAdmin, password);
//		await _userManager.AddToRoleAsync(employeeAdmin, "Admin");
//	}
//}
//TODO: use jwt ?

app.Run();
