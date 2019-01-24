using EF_V0.Controllers.Responses;
using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;
using EF_V0.Core.Services.Interfaces;
using EF_V0.DataBase.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EF_V0.Controllers
{
	/// <summary>
	/// </summary>
	/// <error-code>01</error-code>
	[Authorize]
	[Route("api/auth")]
	[ApiController]
	public class AuthController : BaseController
	{
		protected override string ErrorCode => "01";

		private readonly IAuthService authService;
		private readonly new IUnitOfWork unitOfWork;
		private readonly new IHttpContextAccessor httpContextAccessor;

		public AuthController(IUnitOfWork unitOfWork, IAuthService authService, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.authService = authService;
			this.httpContextAccessor = httpContextAccessor;
		}


		/// <summary>
		/// </summary>
		/// <error-code>01</error-code>
		/// <param name="login User Credential"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost()]
		public IActionResult Login([FromBody] LoginUserDto loginUser)
		{
			string errCode = "01";
			Client client = new Client(unitOfWork, httpContextAccessor);
			if (!client.IsValid(loginUser))
			{
				return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = ErrorCode+errCode+"01",
							Title = "Invalid Client",
							Detail = "Client info is incorrect."
						} }).ToActionResult();
			}

			User user = new User(unitOfWork);
			if (authService.Authenticate(loginUser, ref user))
			{
				// check user
				if (!user.IsActive)
				{
					return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = ErrorCode+errCode+"04",
							Detail = "Your account is suspended"
						} }).ToActionResult();
				}

				if (!user.IsEmailVerified)
				{
					return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = ErrorCode+errCode+"05",
							Detail = "Your email is not verified"
						} }).ToActionResult();
				}

				var payload = new
				{
					authToken = authService.Login(client, user)
				};
				return new Response(HttpStatusCode.Accepted, payload).ToActionResult();
			}
			else
			{
				if (loginUser.GrantType.ToLower().Equals("refreshtoken"))
				{
					return new Response(HttpStatusCode.Forbidden,
					new Error[] { new Error {
						Code = ErrorCode+errCode+"02",
						Detail = "Refresh token is incorrect or expired."
					} }).ToActionResult();
				}
				return new Response(HttpStatusCode.Forbidden,
					new Error[] { new Error {
						Code = ErrorCode+errCode+"03",
						Detail = "Email or password is incorrect."
					} }).ToActionResult();
			}

		}

		[HttpDelete()]
		public IActionResult Logout()
		{
			string errCode = "02";
			if (authService.Logout(GetUserClientId()))
			{

				return new Response(HttpStatusCode.Accepted, null).ToActionResult();
			}

			return new Response(HttpStatusCode.BadGateway,
						new Error[] { new Error {
							Code = ErrorCode+errCode+"01",
							Title = "Invalid data.",
							Detail = "Maybe you have signed out before!"
						} }).ToActionResult();
		}

		[HttpDelete("all")]
		public IActionResult LogoutAll()
		{
			string errCode = "03";
			if (authService.Logout(GetUserClientId(), true))
			{
				return new Response(HttpStatusCode.Accepted, null).ToActionResult();
			}

			return new Response(HttpStatusCode.BadGateway,
						new Error[] { new Error {
							Code = ErrorCode+errCode+"01",
							Title = "Invalid data.",
							Detail = "Maybe you have signed out before!"
						} }).ToActionResult();
		}
	}
}