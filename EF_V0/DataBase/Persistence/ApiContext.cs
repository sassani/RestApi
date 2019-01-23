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

		public DbSet<ClientDb> Client { get; set; }
		public DbSet<UserDb> User { get; set; }
		public DbSet<RoleDb> Role { get; set; }
		public DbSet<UserClientDb> UserClient { get; set; }
		public DbSet<UserRoleDb> UserRole { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Database seeding
			#region Client
			modelBuilder.Entity<ClientDb>().HasData(new ClientDb
			{
				Id = 1,
				ClientPublicId = "GXgxzau9",
				ClientSecret = "GXgxzau9GXgxzau9",
				Name = "localhost",
				Type = EF_V0.Core.AppEnums.ClientType.Web
			});
			#endregion

			#region User
			modelBuilder.Entity<UserDb>().HasData(new UserDb
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
			modelBuilder.Entity<UserDb>().HasData(new UserDb
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
			modelBuilder.Entity<RoleDb>().HasData(new RoleDb
			{
				Id = 1,
				Name = "super_admin",
			});
			modelBuilder.Entity<RoleDb>().HasData(new RoleDb
			{
				Id = 2,
				Name = "admin",
			});
			modelBuilder.Entity<RoleDb>().HasData(new RoleDb
			{
				Id = 3,
				Name = "reader",
			});
			modelBuilder.Entity<RoleDb>().HasData(new RoleDb
			{
				Id = 4,
				Name = "writer",
			});
			#endregion

			#region UserRole
			modelBuilder.Entity<UserRoleDb>().HasData(new UserRoleDb
			{
				Id = 1,
				UserId = 1,
				RoleId = 1
			});
			modelBuilder.Entity<UserRoleDb>().HasData(new UserRoleDb
			{
				Id = 2,
				UserId = 1,
				RoleId = 2
			});
			modelBuilder.Entity<UserRoleDb>().HasData(new UserRoleDb
			{
				Id = 3,
				UserId = 2,
				RoleId = 1
			});
			#endregion

		}

	}
}