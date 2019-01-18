using EF_V0.DataBase.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace EF_V0.DataBase.Persistence
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{ }

		public DbSet<User> User { get; set; }
		public DbSet<Role> Role { get; set; }
		public DbSet<UserRole> UserRole { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Database seeding
			#region User
			modelBuilder.Entity<User>().HasData(new User
			{
				Id = 1,
				PublicId = "fsreytvgvcygec764cyygg",
				FirstName = "Ardavan",
				LastName = "Sassani",
				Email = "a.sassani@gmail.com",
				Password = "$5$10$NQ6DTKjHPnhZqESJXdRzXi$fyASLJbGHwyQVSJim6iIxVzvWRPYtWoF+IK5z2xgKPA=",
				IsActive = true,
				IsEmailVerified = true,
				LastLoginAt = DateTime.Now
			});
			modelBuilder.Entity<User>().HasData(new User
			{
				Id = 2,
				PublicId = "gvhfrtsrtdf2gugjh4jhvjh6uhv",
				FirstName = "Paniz",
				LastName = "Mozi",
				Email = "p.mozafari@gmail.com",
				Password = "$5$10$NQ6DTKjHPnhZqESJXdRzXi$fyASLJbGHwyQVSJim6iIxVzvWRPYtWoF+IK5z2xgKPA=",
				IsActive = true,
				IsEmailVerified = true,
				LastLoginAt = DateTime.Now
			});
			#endregion

			#region Role
			modelBuilder.Entity<Role>().HasData(new Role
			{
				Id = 1,
				Name = "super_admin",
			});
			modelBuilder.Entity<Role>().HasData(new Role
			{
				Id = 2,
				Name = "admin",
			});
			modelBuilder.Entity<Role>().HasData(new Role
			{
				Id = 3,
				Name = "reader",
			});
			modelBuilder.Entity<Role>().HasData(new Role
			{
				Id = 4,
				Name = "writer",
			});
			#endregion

			#region UserRole
			modelBuilder.Entity<UserRole>().HasData(new UserRole
			{
				Id = 1,
				UserId = 1,
				RoleId = 1
			});
			modelBuilder.Entity<UserRole>().HasData(new UserRole
			{
				Id = 2,
				UserId = 1,
				RoleId = 2
			});
			modelBuilder.Entity<UserRole>().HasData(new UserRole
			{
				Id = 3,
				UserId = 2,
				RoleId = 1
			});
			#endregion
		}

	}
}