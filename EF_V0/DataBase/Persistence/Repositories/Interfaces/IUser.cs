using EF_V0.DataBase.Core.Domain;


namespace EF_V0.DataBase.Persistence.Repositories.Interfaces
{
	public interface IUser : IRepo<UserDb>
	{
		UserDb FindByEmail(string email);
		UserRoleDb[] GetRoles(UserDb user);
		void UpdateLastLogin(int userId);
	}

}
