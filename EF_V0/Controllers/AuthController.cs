using EF_V0.Core.Models;
using EF_V0.Core.Models.Requests;
using EF_V0.Core.Models.Responses;
using EF_V0.DataBase.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EF_V0.Controllers
{
	/// <summary>
	/// </summary>
	/// <error-code>02</error-code>
	[Route("api/auth")]
	[ApiController]
	public class AuthController : BaseController
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly string errorCode = "01";

		public AuthController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}


		/// <summary>
		/// </summary>
		/// <error-code>01</error-code>
		/// <param name="login User Credential"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginUser loginUser)
		{
			User user = new User(unitOfWork);
			if (user.Authenticate(loginUser))
			{
				// check user
				if (!user.IsActive)
				{
					return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = errorCode+"0103",
							Detail = "Your account is suspended"
						} }).ToActionResult();
				}

				if (!user.IsEmailVerified)
				{
					return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = errorCode+"0104",
							Detail = "Your email is not verified"
						} }).ToActionResult();
				}

				// get token
				var payload = new
				{
					AuthenticatedUser = user,
					accessToken = "AccessToken"
				};
				return new Response(HttpStatusCode.Accepted, payload).ToActionResult();
			}
			else
			{
				return new Response(HttpStatusCode.Forbidden,
					new Error[] { new Error {
						Code = errorCode+"0101",
						Detail = "Email or password is incorrect."
					} }).ToActionResult();
			}

		}
	}
}