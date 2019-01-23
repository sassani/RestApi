using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;
using EF_V0.Core.Helpers;
using EF_V0.Core.Services.Interfaces;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Core.Domain;

namespace EF_V0.Core.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork unitOfWork;

		public AuthService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public bool Authenticate(LoginUserDto loginUser, ref User user)
		{
			UserDb userDb;
			if (loginUser.GrantType.ToLower().Equals("refreshtoken"))
			{
				var dbUserClient = unitOfWork.UserClient.FindByRefreshToken(loginUser.RefreshToken);
				if (dbUserClient != null)
				{
					userDb = dbUserClient.User;
					userDb.UserRole = unitOfWork.User.GetRoles(userDb);
					Mapper.UserMapper(user, userDb);
					user.RefreshToken = loginUser.RefreshToken;
					return true;
				}
			}
			else if (loginUser.GrantType.ToLower().Equals("idtoken"))
			{
				userDb = unitOfWork.User.FindByEmail(loginUser.Email);
				if (userDb != null)
				{
					if (StringHelper.CompareStringToHash(userDb.Password, loginUser.Password))
					{
						Mapper.UserMapper(user, userDb);
						return true;
					}
				}
			}
			return false;
		}

		public void Login(Client client, User user, string refreshToken)
		{
			if (user.RefreshToken.Equals(""))
			{
				unitOfWork.UserClient.Login(client, user, refreshToken);
			}
			unitOfWork.User.UpdateLastLogin(user.Id);
			unitOfWork.Complete();
		}
	}
}
