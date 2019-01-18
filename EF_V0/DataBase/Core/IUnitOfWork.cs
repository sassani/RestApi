using System;
using EF_V0.DataBase.Core.Interfaces;

namespace EF_V0.DataBase.Core
{
	public interface IUnitOfWork : IDisposable
	{
		IUser User { get; }

		int Complete();
	}
}
