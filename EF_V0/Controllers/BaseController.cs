using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using EF_V0.Core.Models.Responses;

namespace EF_V0.Controllers
{
	public class BaseController : Controller
	{
		//protected int GetUserId()
		//{
		//	if (int.TryParse(User.FindFirst("uid")?.Value, out int id))
		//		return id;
		//	else
		//		return 0;
		//}
		//protected User GetUser()
		//{
		//	var unitOfWork = HttpContext.RequestServices.GetService<IUnitOfWork>();

		//	return unitOfWork.Users.Where(table => table.Id == GetUserId()).FirstOrDefault();
		//}

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