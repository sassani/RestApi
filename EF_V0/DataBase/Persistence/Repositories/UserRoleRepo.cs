using EF_V0.DataBase.Core.Domain;
using EF_V0.DataBase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.DataBase.Persistence.Repositories
{
	public class UserRoleRepo:Repo<UserRole>, IUserRole
	{
		public UserRoleRepo(ApiContext context) : base(context)
		{
		}
		public Role[] GetRolesByUserId(int userId)
		{
			Role[] roles = new Role[1];
			return roles;
		}
	}
}
