using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Core.Services.Interfaces
{
	public interface IAuthService
	{
		bool Authenticate(LoginUserDto loginUser, ref User user);
		void Login(Client client, User user, string refreshToken);
	}
}
