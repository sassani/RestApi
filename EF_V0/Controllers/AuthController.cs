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
	/// <error-code>02</error-code>
	[Authorize]
	[Route("api/auth")]
	[ApiController]
	public class AuthController : BaseController
	{
		private readonly ITokenService tokenService;
		private readonly IAuthService authService;
		private readonly new IUnitOfWork unitOfWork;
		private readonly new IHttpContextAccessor httpContextAccessor;
		private readonly string errorCode = "01";

		public AuthController(IUnitOfWork unitOfWork, ITokenService tokenService, IAuthService authService, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.tokenService = tokenService;
			this.authService = authService;
			this.httpContextAccessor = httpContextAccessor;
		}


		/// <summary>
		/// </summary>
		/// <error-code>01</error-code>
		/// <param name="login User Credential"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginUserDto loginUser)
		{
			Client client = new Client(unitOfWork, httpContextAccessor);
			if (!client.IsValid(loginUser))
			{
				return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = errorCode+"0101",
							Title = "Invalid Client",
							Detail = "Client info is incorrect."
						} }).ToActionResult();
			}

			User user = new User(unitOfWork);
			bool authorized = authService.Authenticate(loginUser, ref user);
			//if (loginUser.GrantType.ToLower().Equals("refreshtoken"))
			//{
			//	authorized = user.Authenticate(loginUser.RefreshToken);

			//}
			//else if (loginUser.GrantType.ToLower().Equals("idtoken"))
			//{
			//	authorized = user.Authenticate(loginUser);
			//}

			if (authorized)
			{
				// check user
				if (!user.IsActive)
				{
					return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = errorCode+"0104",
							Detail = "Your account is suspended"
						} }).ToActionResult();
				}

				if (!user.IsEmailVerified)
				{
					return new Response(HttpStatusCode.Forbidden,
						new Error[] { new Error {
							Code = errorCode+"0105",
							Detail = "Your email is not verified"
						} }).ToActionResult();
				}

				// get token
				AuthTokenDto token = tokenService.GenerateAuthToken(user);
				authService.Login(client, user, token.RefreshToken);
				var payload = new
				{
					authToken = token
				};
				return new Response(HttpStatusCode.Accepted, payload).ToActionResult();
			}
			else
			{
				if (loginUser.GrantType.ToLower().Equals("refreshtoken"))
				{
					return new Response(HttpStatusCode.Forbidden,
					new Error[] { new Error {
						Code = errorCode+"0102",
						Detail = "Refresh token is incorrect or expired."
					} }).ToActionResult();
				}
				return new Response(HttpStatusCode.Forbidden,
					new Error[] { new Error {
						Code = errorCode+"0103",
						Detail = "Email or password is incorrect."
					} }).ToActionResult();
			}

		}

		[AllowAnonymous]
		[HttpGet("test")]
		public IActionResult Test()
		{
			var user = GetUser();
			return new Response(HttpStatusCode.Accepted, user).ToActionResult();
		}
	}
}