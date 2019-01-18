using EF_V0.DataBase.Core.Domain;
using EF_V0.DataBase.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.DataBase.Persistence.Repositories
{
	public class UserRepo : Repo<User>, IUser
	{
		private new ApiContext context;
		public UserRepo(ApiContext context) : base(context)
		{
			this.context = context;
		}

		public User FindByEmail(string email)
		{
			return context.User
				.Where(u => u.Email.ToLower() == email.ToLower())
				.Include(u => u.UserRole)
				.ThenInclude(r => r.Role)
				.SingleOrDefault<User>();
		}

		public Role[] GetRoles(User user)
		{
			
			return user.UserRole.Select(r => r.Role).ToArray();
		}

	}
}
