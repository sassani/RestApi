
using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;

namespace EF_V0.Core.Services.Interfaces
{
	public interface IClientService
	{
		Client CreateClient(LoginUserDto loginUser);
	}
}
