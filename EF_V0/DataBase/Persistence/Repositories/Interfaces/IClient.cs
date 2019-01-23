using EF_V0.DataBase.Core.Domain;


namespace EF_V0.DataBase.Persistence.Repositories.Interfaces
{
	public interface IClient : IRepo<ClientDb>
	{
		ClientDb FindByClientPublicId(string clientId);
	}
}
