using Rsk.AspNetCore.Fido;
using Rsk.AspNetCore.Fido.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Passwordless_Authn.Models;

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

        [Authorize]
        public IActionResult Index()
        {
            return View();
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
    }
}
