using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace EmployeesManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		//[Authorize(Roles = "Admin")] TODO: move to admin controller?
		[HttpPost("registerNewEmployee")]
		public async Task<IActionResult> Register(EmployeeRegisterDto employeeRegisterDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _authService.RegisterEmployeeAsync(employeeRegisterDto);

				if (result.Success)
				{
					return Ok("Registered successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(EmployeeLoginDto employeeLoginDto)
		{
			try
			{
				var result = await _authService.LoginEmployeeAsync(employeeLoginDto);
				if (result.Success)
				{
					return Ok("Login successful");
				}
				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			try
			{
				await _authService.LogoutEmployeeAsync();
				return Ok("Succses");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpGet("Profile")]
		public async Task<IActionResult> Profile()
		{
			try
			{
				var result = await _authService.GetEmployeeProfileAsync();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Authorize]
		[HttpPut("ChangePassword")]
		public async Task<IActionResult> ChangePassword(string password)
		{
			if (password == null)
			{
				return BadRequest("Password can not be empty");
			}
			try
			{
				var result = await _authService.ChangePasswordAsync(password);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
