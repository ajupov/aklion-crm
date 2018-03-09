﻿using Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return IsUserContextInitialized
                ? (IActionResult) RedirectToAction("Index", UserContext.Permissions.Contains(Permission.Admin)
                    ? "Console"
                    : "UserConsole")
                : View();
        }
    }
}