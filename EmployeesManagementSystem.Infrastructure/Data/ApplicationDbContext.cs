using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<RequestState> RequestStates { get; set; }
		public DbSet<VacationRequest> VacationRequests { get; set; }
		public DbSet<VacationType> VacationTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.HasKey(e => e.EmployeeNumber);

				entity.Property(e => e.EmployeeNumber)
									  .HasMaxLength(6)
									  .IsRequired()
									  .HasColumnType("varchar(6)");

				entity.Property(e => e.EmployeeName)
					  .HasMaxLength(20)
					  .IsRequired();

				entity.HasIndex(e => e.EmployeeName).IsUnique();

				entity.Property(e => e.GenderCode)
					  .HasMaxLength(1)
					  .IsRequired()
					  .HasColumnType("char(1)");

				entity.Property(e => e.Salary)
					  .HasColumnType("decimal(10,2)");

				entity.Property(e => e.ReportedToEmployeeNumber)
					  .HasMaxLength(6)
					  .IsRequired(false)
					  .HasColumnType("varchar(6)");

				entity.Property(e => e.VacationDaysLeft)
					  .HasDefaultValue(24);

				entity.HasOne(e => e.ReportedToEmployee)
					  .WithMany(e => e.Subordinates)
					  .HasForeignKey(e => e.ReportedToEmployeeNumber)
					  .IsRequired(false);
			});

			modelBuilder.Entity<VacationRequest>()
				.HasOne(v => v.ApprovedBy)
				.WithMany(e => e.ApprovedVacationRequests)
				.HasForeignKey(v => v.ApprovedByEmployeeNumber)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<VacationRequest>()
				.HasOne(v => v.DeclinedBy)
				.WithMany(e => e.DeclinedVacationRequests)
				.HasForeignKey(v => v.DeclinedByEmployeeNumber)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<VacationRequest>()
			.Property(e => e.RequestSubmissionDate)
			.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<VacationRequest>()
	.HasOne(v => v.VacationType)
	.WithMany(vt => vt.VacationRequests)
	.HasForeignKey(v => v.VacationTypeCode)
	.HasPrincipalKey(vt => vt.VacationTypeCode);

			modelBuilder.Entity<VacationRequest>()
				.HasOne(v => v.Employee)
				.WithMany()
				.HasForeignKey(v => v.EmployeeNumber)
				.HasPrincipalKey(e => e.EmployeeNumber);

			modelBuilder.Entity<Employee>()
						.HasOne(e => e.ApplicationUser)
						.WithOne(a => a.Employee)
						.HasForeignKey<Employee>(e => e.UserId)
						.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Employee>().ToTable("Employees");

			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.EmailConfirmed);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.SecurityStamp);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.ConcurrencyStamp);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.PhoneNumber);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.PhoneNumberConfirmed);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.TwoFactorEnabled);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.LockoutEnd);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.LockoutEnabled);
			modelBuilder.Entity<ApplicationUser>().Ignore(v => v.AccessFailedCount);

		}
	}
}
