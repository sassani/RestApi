using EF_V0.DataBase.Core.Domain;


namespace EF_V0.DataBase.Core.Interfaces
{
	public interface IUser : IRepo<User>
	{
		User FindByEmail(string email);
		Role[] GetRoles(User user);
	}

}
