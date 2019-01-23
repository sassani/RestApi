using EF_V0.DataBase.Core.Domain;
using EF_V0.DataBase.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF_V0.DataBase.Persistence.Repositories
{
	public class ClientRepo : Repo<ClientDb>, IClient
	{
		private new ApiContext context;
		public ClientRepo(ApiContext context) : base(context)
		{
			this.context = context;
		}

		public ClientDb FindByClientPublicId(string publicId)
		{
			return context.Client
				.Where(a => a.ClientPublicId == publicId)
				.SingleOrDefault();
		}
	}
}
