
using EF_V0.Controllers.Responses;
using EF_V0.Core.Entities;
using EF_V0.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EF_V0.Controllers
{
	public class BaseController : Controller
	{
		protected string ErrorCode { get; set; }

		protected readonly IUserService userSrvice;

		protected BaseController(IUserService userSrvice)
		{
			this.userSrvice = userSrvice;
		}

		protected int GetUserClientId()
		{
			if (int.TryParse(User.FindFirst("aid")?.Value, out int id))
				return id;
			else
				return 0;
		}

		protected int GetUserId()
		{
			if (int.TryParse(User.FindFirst("uid")?.Value, out int id))
				return id;
			else
				return 0;
		}

		protected User GetUser()
		{
			return userSrvice.Get(GetUserId());
		}

		protected IActionResult MakeResponse(System.Net.HttpStatusCode statusCode, object payload = null)
		{
			return new Response(statusCode, payload).ToActionResult();
		}

		protected IActionResult MakeErrorResponse(System.Net.HttpStatusCode statusCode, Error error)
		{
			return new Response(statusCode, new Error[] { error }).ToActionResult();
		}
	}
}