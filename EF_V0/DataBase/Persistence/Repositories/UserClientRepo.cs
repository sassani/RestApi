using EF_V0.Core.Entities;
using EF_V0.DataBase.Core.Domain;
using EF_V0.DataBase.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EF_V0.DataBase.Persistence.Repositories
{
	public class UserClientRepo : Repo<UserClientDb>, IUserClient
	{
		private readonly new ApiContext context;


		public UserClientRepo(ApiContext context) : base(context)
		{
			this.context = context;
		}
		public UserClientDb FindByRefreshToken(string refreshToken)
		{
			DateTime currentDay = DateTime.Now;

			return context.UserClient
				.Where(uc => uc.RefreshToken == refreshToken && currentDay - uc.CreatedAt <= TimeSpan.FromDays(73))
				.Include(u => u.User)
				.ThenInclude(uc=>uc.UserRole)
				.SingleOrDefault();
		}

		public void Login(Client client, User user, string refreshToken)
		{
			UserClientDb userClient = new UserClientDb()
			{
				RefreshToken = refreshToken,
				UserId = user.Id,
				ClientId = client.Id,
				Platform = client.Platform,
				Browser = client.Browser,
				IP = client.IP,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};
			context.UserClient.Add(userClient);
		}
	}
}
