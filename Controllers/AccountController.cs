using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Passwordless_Authn.Models;
using Microsoft.AspNetCore.Authentication;

namespace Passwordless_Authn.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
	{ 

        [HttpGet, Route("Login")]
        public IActionResult Login()
        {
			if (User.Identity.IsAuthenticated)
			{
				return Content("You are already authorized!");
			}

			return View();
        }

		[HttpPost, Route("Login")]
		public async Task<IActionResult> Login(string? returnUrl, string? email, string? password)
		{

			if (email is null || password is null)
			{
				return new BadRequestObjectResult("Email or password is empty.");
			}

			LoginModel? res = people.FirstOrDefault(p => p.Email == email && p.Password == password);
			if (res is null)
			{
				return new UnauthorizedObjectResult("Invalid email or password.");
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, res.Email)
			};
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Password");
			await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
			string redirUrl = returnUrl ?? "/";
			return Redirect(redirUrl);
		}

		[HttpGet, Route("Logout")]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("Cookies");
			return Redirect("\\");
		}

		internal static IEnumerable<LoginModel> people = new List<LoginModel>
		{
			new LoginModel("tom@gmail.com", "12345"),
			new LoginModel("bob@gmail.com", "55555"),
			new LoginModel("anton.zhidovich@gmail.com", "7777"),
			new LoginModel("alexlubenko172@gmail.com", "1313")
		};

	}
}
