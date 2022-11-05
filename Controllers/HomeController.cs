using Microsoft.AspNetCore.Mvc;

namespace Passwordless_Authn.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/Index"), Route("/")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
