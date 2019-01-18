using EF_V0.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Core.Models
{
	/// <summary>
	/// Describe your class quickly here.
	/// </summary>
	/// <remarks>
	/// Add more details here.
	/// </remarks>
	public class Client
	{
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }

		//public Application Application { get; private set; }

		//private readonly IUnitOfWork unitOfWork;
		//private readonly IConfiguration configuration;
		private readonly HttpContext context;

		public Client(HttpContext context)
		{
			//unitOfWork = context.RequestServices.GetService<IUnitOfWork>();
			//configuration = context.RequestServices.GetService<IConfiguration>();
			this.context = context;
		}

		public bool Authorize()
		{
			// validate client with clientId and ClientSecret

			// get 
			//var application = unitOfWork.Applications.Where(x => x.ClientId == ClientId).FirstOrDefault();
			//if (application == null)
			//{
			//	return false;
			//}

			//if (application.Type == Application.Types.Web)
			//{
			//	string origin = "";
			//	if (context.Request.Headers.ContainsKey("Origin"))
			//	{
			//		origin = context.Request.Headers["Origin"].First();
			//	}

			//	string[] validOrigins = configuration.GetSection("CrosUrl").Get<string[]>();
			//	if (validOrigins.Contains(origin))
			//	{
			//		Application = application;
			//		return true;
			//	}
			//}
			//else
			//{
			//	if (PasswordManager.Compare(application.ClientSecret, ClientSecret))
			//	{
			//		Application = application;
			//		return true;
			//	}
			//}

			return false;
		}
	}
}
