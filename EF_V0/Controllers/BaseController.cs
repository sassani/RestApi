
using EF_V0.Controllers.Responses;
using EF_V0.Core.Entities;
using EF_V0.DataBase.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_V0.Controllers
{
	public abstract class BaseController : Controller
	{
		protected abstract string ErrorCode { get; }

		protected readonly IUnitOfWork unitOfWork;
		protected readonly IHttpContextAccessor httpContextAccessor;

		protected BaseController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.httpContextAccessor = httpContextAccessor;
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
			User user = new User(unitOfWork);
			user.GetUser(GetUserId());
			return user;
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