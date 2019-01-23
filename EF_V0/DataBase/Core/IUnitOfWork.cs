using System;
using EF_V0.DataBase.Persistence.Repositories.Interfaces;

namespace EF_V0.DataBase.Core
{
	public interface IUnitOfWork : IDisposable
	{
		IClient Client { get; }
		IUser User { get; }
		IUserClient UserClient { get; }

		int Complete();
	}
}
