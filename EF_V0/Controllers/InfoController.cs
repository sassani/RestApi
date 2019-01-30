using EF_V0.Controllers.Responses;
using EF_V0.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EF_V0.Controllers
{
	[AllowAnonymous]
	[Route("api/info")]
	[ApiController]
	public class InfoController : BaseController
	{
		public InfoController(IUserService userSrvice): base(userSrvice)
		{

		}
		[HttpGet()]
		public IActionResult GetInfo()
		{
			var payload = new
			{
				Data = "This is initial data"
			};
			return new Response(HttpStatusCode.Accepted, payload).ToActionResult();
		}
	}
}