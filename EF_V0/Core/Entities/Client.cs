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

		private readonly IUnitOfWork unitOfWork;
		private readonly IHttpContextAccessor httpContextAccessor;

		public Client(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.httpContextAccessor = httpContextAccessor;
		}

		public bool IsValid(LoginUserDto loginUser)
		{
			var dbClient = unitOfWork.Client.FindByClientPublicId(loginUser.ClientId);
			if (dbClient == null)
			{
				return false;
			}

			if (dbClient.Type.Equals(AppEnums.ClientType.Mobile))
			{
				if (!dbClient.ClientSecret.Equals(loginUser.ClientSecret))
				{
					return false;
				}
			}

			// TODO: set client properties (map dbClient to Client model)
			Mapper.MapDbModelToClassModel(this, dbClient);
			IP = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
			ClientParser();
			return true;
		}

		private void ClientParser()
		{
			/// ref:https://github.com/ua-parser/uap-csharp
			string uaString = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

			// get a parser with the embedded regex patterns
			var uaParser = Parser.GetDefault();

			// get a parser using externally supplied yaml definitions
			// var uaParser = Parser.FromYaml(yamlString);

			ClientInfo clientInfo = uaParser.Parse(uaString);

			Browser = clientInfo.UserAgent.Family + " " + clientInfo.UserAgent.Major + "." + clientInfo.UserAgent.Minor;
			if(Browser == "Other .")
			{
				Browser = uaString;
			}

			if (clientInfo.Device.Family != "Other")
				Platform = clientInfo.Device.Family + " " + clientInfo.Device.Model + " ";

			Platform += clientInfo.OS.Family;

			if (clientInfo.OS.Major != null)
			{
				Platform += " " + clientInfo.OS.Major + "." + clientInfo.OS.Minor;
			}

		}
		
	}
}
