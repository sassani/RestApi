
using EF_V0.DataBase.Core.Domain;

namespace EF_V0.DataBase.Core.Interfaces
{
	public interface IUserRole : IRepo<UserRole>
	{
		Role[] GetRolesByUserId(int userId);
	}
}
