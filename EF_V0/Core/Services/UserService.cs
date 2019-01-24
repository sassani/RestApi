using EF_V0.Core.Entities;
using EF_V0.Core.Helpers;
using EF_V0.Core.Services.Interfaces;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork unitOfWork;
		public UserService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public User Get(int userId)
		{
			User user = new User();
			UserDb userDb = unitOfWork.User.Get(userId);
			Mapper.UserMapper(user, userDb);
			return user;
		}
	}
}
