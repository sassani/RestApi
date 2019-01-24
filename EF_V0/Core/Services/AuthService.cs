using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;
using EF_V0.Core.Helpers;
using EF_V0.Core.Services.Interfaces;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Core.Domain;
using System;

namespace EF_V0.Core.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly ITokenService tokenSrvice;
		private string refreshToken;
		private int userClientId;

		public AuthService(IUnitOfWork unitOfWork, ITokenService tokenSrvice)
		{
			this.unitOfWork = unitOfWork;
			this.tokenSrvice = tokenSrvice;
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
					refreshToken = loginUser.RefreshToken;
					userClientId = dbUserClient.Id;
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

		public AuthTokenDto Login(Client client, User user)
		{
			if (refreshToken == null)
			{
				refreshToken = tokenSrvice.GenerateRefreshToken(user.PublicId);
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
				unitOfWork.UserClient.Add(userClient);
				unitOfWork.Complete();
				userClientId = userClient.Id;
			}
			unitOfWork.User.UpdateLastLogin(user.Id);
			return tokenSrvice.GenerateAuthToken(user, userClientId, refreshToken);
		}

		public bool Logout(int userClientId, bool all=false)
		{
			var ucdb = unitOfWork.UserClient.Get(userClientId);
			if (ucdb != null && all)
			{
				var ucdbs = unitOfWork.UserClient.Find(uc => uc.ClientId == ucdb.ClientId && uc.UserId == ucdb.UserId);
				unitOfWork.UserClient.RemoveRange(ucdbs);
				unitOfWork.Complete();
				return true;
			}
			if (ucdb != null)
			{
				unitOfWork.UserClient.Remove(ucdb);
				unitOfWork.Complete();
				return true;
			}
			return false;
		}
	}
}




