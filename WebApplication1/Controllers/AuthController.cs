using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Cliente");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Usuario usuario, string ReturnUrl)
        {
            if (IsValidAdmin(usuario))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Email), 
                    new Claim(ClaimTypes.Role, "Admin"), 

                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = usuario.KeepLogeedIn
                };
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
                return RedirectToAction("Index", "Cliente");
            }
            if (IsValidUser(usuario))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Email), 
                    new Claim(ClaimTypes.Role, "User"), 

                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = usuario.KeepLogeedIn
                };
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
                return RedirectToAction("Index", "Cliente");
            }
            ViewData["ValidateMessage"] = "Credienciales Incorrectas.";
            return View();
        }
        public IActionResult AccessDenied () { return View(); } 
        private bool IsValidAdmin(Usuario usuario)
        {
            if (usuario.Email == "admin@admin.com" && usuario.Password == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsValidUser(Usuario usuario)
        {
            if (usuario.Email == "jrjacomes@uce.edu.ec" && usuario.Password == "1234")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}
