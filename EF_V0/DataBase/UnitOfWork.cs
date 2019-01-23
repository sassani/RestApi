//using System;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Persistence.Repositories.Interfaces;
using EF_V0.DataBase.Persistence;
using EF_V0.DataBase.Persistence.Repositories;

namespace EF_V0.DataBase
{
	/// <summary>
	/// All needed Database transactions mus be handled by this class
	/// </summary>
	/// <remarks>
	/// Add more details here.
	/// </remarks>
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly ApiContext context;

		public IUser User { get; }
		public IClient Client { get; }
		public IUserClient UserClient { get; set; }

		public UnitOfWork(ApiContext context)
		{
			this.context = context;
			User = new UserRepo(context);
			Client = new ClientRepo(context);
			UserClient = new UserClientRepo(context);
		}

		public int Complete()
		{
			return context.SaveChanges();
		}

		public void Dispose()
		{
			context.Dispose();
		}
	}
}
