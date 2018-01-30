using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public async Task SignInAsync(int userId, int storeId, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid, userId.ToString(), ClaimValueTypes.Integer),
                new Claim("StoreId", storeId.ToString(), ClaimValueTypes.Integer),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                AllowRefresh = false
            };

            await HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, properties)
                .ConfigureAwait(false);
        }

        [NonAction]
        public Task SignOutAsync()
        {
            UserContext = null;
            ViewBag.UserContext = null;
            ViewBag.IsUserContextInitialized = false;
            return HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [NonAction]
        public IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public void InitializeUserContext(UserContextModel userContextDomain)
        {
            UserContext = new UserContext.UserContext
            {
                UserId = userContextDomain.CurrentUser.Id,
                UserLogin = userContextDomain.CurrentUser.Login,
                IsEmailConfirmed = userContextDomain.CurrentUser.IsEmailConfirmed,
                IsPhoneConfirmed = userContextDomain.CurrentUser.IsPhoneConfirmed,
                IsLocked = userContextDomain.CurrentUser.IsLocked,
                IsDeleted = userContextDomain.CurrentUser.IsDeleted,
                AvatarUrl = userContextDomain.CurrentUser.AvatarUrl,
                StoreId = userContextDomain.CurrentStore?.Id ?? 0,
                StoreName = userContextDomain.CurrentStore?.Name,
                StoreIsLocked = userContextDomain.CurrentStore?.IsLocked ?? false,
                StoreIsDeleted = userContextDomain.CurrentStore?.IsDeleted ?? false,
                Permissions = userContextDomain.CurrentStorePermissions?.Select(s => s.Permission).ToList()
            };

            ViewBag.UserContext = UserContext;
            ViewBag.IsUserContextInitialized = IsUserContextInitialized;
        }

        public UserContext.UserContext UserContext { get; set; }

        public bool IsUserContextInitialized => UserContext != null && UserContext.UserId > 0;
    }
}