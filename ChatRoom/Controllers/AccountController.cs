using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatRoom.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string provider, string returnUrl = "/")
        {
            if (string.IsNullOrEmpty(provider))
            {
                return BadRequest();
            }
            var redirectUrl = Url.Action(nameof(LoginCallback), new { returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> LoginCallback(string returnUrl = "/")
        {
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
