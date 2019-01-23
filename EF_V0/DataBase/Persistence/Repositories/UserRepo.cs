using EF_V0.DataBase.Core.Domain;
using EF_V0.DataBase.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace EF_V0.DataBase.Persistence.Repositories
{
	public class UserRepo : Repo<UserDb>, IUser
	{
		private readonly new ApiContext context;
		public UserRepo(ApiContext context) : base(context)
		{
			this.context = context;
		}

		public UserDb FindByEmail(string email)
		{
			return context.User
				.Where(u => u.Email.ToLower() == email.ToLower())
				.Include(u => u.UserRole)
				.ThenInclude(r => r.Role)
				.SingleOrDefault();
		}

		public UserRoleDb[] GetRoles(UserDb user)
		{
			return context.UserRole
				.Where(ur => ur.UserId == user.Id)
				.Include(r => r.Role)
				.ToArray();
		}

		public void UpdateLastLogin(int userId)
		{
			var user = context.User.SingleOrDefault(u => u.Id == userId);
			user.LastLoginAt = DateTime.Now;
			context.User.Update(user);
		}



	}
}
