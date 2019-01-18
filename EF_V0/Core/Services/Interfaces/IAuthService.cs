using EF_V0.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Core.Services.Interfaces
{
	public interface IAuthService
	{
		string GenerateToken(User user);
		bool ValidateToken(string TokenString);
	}
}
