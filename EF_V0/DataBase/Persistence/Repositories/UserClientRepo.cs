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
				.ThenInclude(uc => uc.UserRole)
				.SingleOrDefault();
		}

		//public void Login(UserClientDb userClient)
		//{
		//	context.UserClient.Add(userClient);
		//	int userClientId = userClient.Id;
		//}

		//public void Logout(UserClientDb userClient)
		//{
		//	//const string query = "DELETE FROM [dbo].["++"] WHERE [id]={0}";
		//	//var rows = context.Database.ExecuteSqlCommand(query, id);
		//	//var userClients = context.UserClient.Where(uc => uc.UserId == user.Id && uc.ClientId == client.Id).ToArray();
		//	//context.UserClient.RemoveRange(userClients);
		//}
	}
}
