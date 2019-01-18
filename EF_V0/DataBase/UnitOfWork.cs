//using System;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Core.Interfaces;
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
		private readonly ApiContext _context;

		public IUser User { get; }

		public UnitOfWork(ApiContext context)
		{
			_context = context;
			User = new UserRepo(context);

		}

		public int Complete()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
