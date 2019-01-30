using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;
using EF_V0.Helpers;
using EF_V0.Core.Services.Interfaces;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Core.Domain;
using Microsoft.AspNetCore.Http;
using System;
using UAParser;

namespace EF_V0.Core.Services
{
	public class ClientService : IClientService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IHttpContextAccessor httpContextAccessor;

		public ClientService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.httpContextAccessor = httpContextAccessor;

		}

		public Client CreateClient(LoginUserDto loginUser)
		{
			Client client = new Client
			{
				ClientPublicId = loginUser.ClientId,
				ClientSecret = loginUser.ClientSecret,
			};
			client.IsValid = CheckValidation(client);
			return client;
		}

		private bool CheckValidation(Client client)
		{
			ClientDb clientDb = unitOfWork.Client.FindByClientPublicId(client.ClientPublicId);
			if (clientDb == null)
			{
				return false;
			}

			if (clientDb.Type.Equals(AppEnums.ClientType.Mobile))
			{
				if (!clientDb.ClientSecret.Equals(client.ClientSecret))
				{
					return false;
				}
			}

			Mapper.MapDbModelToClassModel(client, clientDb);
			client.IP = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
			ClientParser(client);
			return true;
		}

		private void ClientParser(Client client)
		{
			/// ref:https://github.com/ua-parser/uap-csharp
			string uaString = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

			// get a parser with the embedded regex patterns
			var uaParser = Parser.GetDefault();

			// get a parser using externally supplied yaml definitions
			// var uaParser = Parser.FromYaml(yamlString);

			ClientInfo clientInfo = uaParser.Parse(uaString);

			client.Browser = clientInfo.UserAgent.Family + " " + clientInfo.UserAgent.Major + "." + clientInfo.UserAgent.Minor;
			if (client.Browser == "Other .")
			{
				client.Browser = uaString;
			}

			if (clientInfo.Device.Family != "Other")
				client.Platform = clientInfo.Device.Family + " " + clientInfo.Device.Model + " ";

			client.Platform += clientInfo.OS.Family;

			if (clientInfo.OS.Major != null)
			{
				client.Platform += " " + clientInfo.OS.Major + "." + clientInfo.OS.Minor;
			}

		}
	}
}
