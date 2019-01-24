using EF_V0.Core.Entities;


namespace EF_V0.Core.Services.Interfaces
{
	public interface IUserService
	{
		User Get(int userId);
	}
}
