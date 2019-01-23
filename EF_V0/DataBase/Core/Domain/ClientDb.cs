using EF_V0.Core.Entities.DTOs;
using System.Collections.Generic;
using static EF_V0.Core.AppEnums;

namespace EF_V0.DataBase.Core.Domain
{
	public class ClientDb
	{
		public int Id { get; set; }
		public string ClientPublicId { get; set; }
		public string ClientSecret { get; set; }
		public string Name { get; set; }
		public ClientType Type { get; set; }

		public virtual ICollection<UserClientDb> UserClient { get; set; }

	}
}
