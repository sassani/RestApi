using EF_V0.Core.Entities;
using EF_V0.DataBase.Core.Domain;


namespace EF_V0.DataBase.Persistence.Repositories.Interfaces
{
	public interface IUserClient : IRepo<UserClientDb>
	{
		void Login(Client client, User user, string refreshToken);
		UserClientDb FindByRefreshToken(string refreshToken);
	}
}
