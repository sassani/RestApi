using EF_V0.Core.Entities.DTOs;
using EF_V0.Core.Helpers;
using EF_V0.DataBase.Core;
using Microsoft.AspNetCore.Http;
using UAParser;

namespace EF_V0.Core.Entities
{
	public class Client
	{
		public int Id { get; set; }
		public string ClientPublicId { get; set; }
		public string ClientSecret { get; set; }
		public string Name { get; set; }
		public AppEnums.ClientType Type { get; set; }

		public string Platform { get; set; }
		public string Browser { get; set; }
		public string IP { get; set; }

		public bool  IsValid { get; set; }

		private readonly IUnitOfWork unitOfWork;
		private readonly IHttpContextAccessor httpContextAccessor;

		public Client(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.httpContextAccessor = httpContextAccessor;
		}

		public Client()
		{
		}

	}
}
