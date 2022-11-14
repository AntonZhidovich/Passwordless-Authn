using Rsk.AspNetCore.Fido;
using Rsk.AspNetCore.Fido.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Passwordless_Authn.Models;
using System.Security.Claims;

namespace Passwordless_Authn.Controllers
{
    [Route("Fido")]
    public class FidoController : Controller
    {
        private readonly IFidoAuthentication fido;
        public FidoController(IFidoAuthentication fido)
        {
            this.fido = fido;
        }

        [Authorize, HttpGet, Route("StartRegistration")]
        public IActionResult StartRegistration()
        {
            return View();
        }

        [Authorize, HttpPost, Route("Register")]
        public async Task<IActionResult> Register(DeviceModel model)
        {
            var challenge = await fido.InitiateRegistration(User.Identity.Name, model.Name);
            return View(DtoHelpers.ToBase64Dto(challenge));
        }

        [Authorize, HttpPost, Route("CompleteRegistration")]
        public async Task<IActionResult> CompleteRegistration(
            [FromBody] Base64FidoRegistrationResponse response)
        {
            var result = await fido.CompleteRegistration(response.ToFidoResponse());
            if (result.IsError)
            {
                return BadRequest(result.IsError);
            }

            return Ok();
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(string? userId)
        {
            if (userId is null)
                return BadRequest("Empty userId.");

            LoginModel? res = AccountController.people.FirstOrDefault(p => p.Email == userId);
            if (res is null)
            {
                return new UnauthorizedObjectResult("Invalid email.");
            }

            try
            {
                var challenge = await fido.InitiateAuthentication(userId);
                return View(challenge.ToBase64Dto());
            }
            catch (PublicKeyCredentialException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("CompleteLogin")]
        public async Task<IActionResult> CompleteLogin(
            [FromBody] Base64FidoAuthenticationResponse authenticationResponse)
        {
            var result = await fido.CompleteAuthentication(authenticationResponse.ToFidoResponse());
            if (result.IsSuccess)
            {
                LoginModel? res = AccountController.people.First(p => p.Email == result.UserId);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, res.Email)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "WebAuthn");
                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity));
            }
            else
            {
                return BadRequest("Error in completing the authentication.");
            }
            return Redirect("/");
        }
    }
}
