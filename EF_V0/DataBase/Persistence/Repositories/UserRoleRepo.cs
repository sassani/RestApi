using EF_V0.DataBase.Core.Domain;
using EF_V0.DataBase.Persistence.Repositories.Interfaces;

namespace EF_V0.DataBase.Persistence.Repositories
{
	public class UserRoleRepo : Repo<UserRoleDb>, IUserRole
	{
		public UserRoleRepo(ApiContext context) : base(context)
		{
		}
	}
}
