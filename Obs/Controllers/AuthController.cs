using System.Security.Claims;
using ApiLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obs.Models;

namespace Obs.Controllers
{
    public class AuthController : Controller
    {
        private readonly OgrenciApiService _ogrenciService;
        private readonly RolesApiService _rolesService;

        public AuthController(OgrenciApiService ogrenciService, RolesApiService rolesService)
        {
            _ogrenciService = ogrenciService;
            _rolesService = rolesService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToLocal(returnUrl);
            }

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<isKatmani.Ogrenci> ogrenciler;
            try
            {
                ogrenciler = await _ogrenciService.GetAllAsync();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Giris servisine ulasilamadi. API calisiyor mu kontrol edin.");
                return View(model);
            }

            var normalizedUserName = Normalize(model.UserName);
            var user = ogrenciler.FirstOrDefault(ogrenci =>
                Normalize(ogrenci.Id.ToString()) == normalizedUserName ||
                Normalize(ogrenci.ad) == normalizedUserName ||
                Normalize($"{ogrenci.ad} {ogrenci.soyad}") == normalizedUserName);

            if (user == null || user.sifre != model.Password)
            {
                ModelState.AddModelError(string.Empty, "Kullanici adi veya sifre hatali.");
                return View(model);
            }

            var roleName = await GetRoleNameAsync(user.roleId);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, $"{user.ad} {user.soyad}".Trim()),
                new(ClaimTypes.Role, roleName),
                new("RoleId", user.roleId.ToString()),
                new("UserType", "Ogrenci")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(7) : null
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return RedirectToLocal(model.ReturnUrl);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        private static string Normalize(string? value)
        {
            return (value ?? string.Empty).Trim().ToUpperInvariant();
        }

        private async Task<string> GetRoleNameAsync(int roleId)
        {
            try
            {
                var roles = await _rolesService.GetAllAsync();
                var role = roles.FirstOrDefault(item => item.Id == roleId);
                return ToCanonicalRole(role?.ad) ?? roleId.ToString();
            }
            catch
            {
                return roleId.ToString();
            }
        }

        private static string? ToCanonicalRole(string? roleName)
        {
            var normalized = Normalize(roleName).Replace(" ", string.Empty);

            return normalized switch
            {
                "SUPERADMIN" => "SuperAdmin",
                "ADMIN" => "Admin",
                "USER" => "User",
                "OGRENCI" => "Ogrenci",
                "OGRENCİ" => "Ogrenci",
                "ÖGRENCI" => "Ogrenci",
                "ÖGRENCİ" => "Ogrenci",
                "ÖĞRENCI" => "Ogrenci",
                "ÖĞRENCİ" => "Ogrenci",
                _ => null
            };
        }
    }
}
