using System;
using Crm.Enums;
using Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    public class HomeController : BaseController
    {
        private readonly string Tag;
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            Tag = GetType().Name;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Info(Tag, "Test message", new {Test = 123}, new Exception("Тестовое исключение",  new Exception("Внутреннее")));

            return IsUserContextInitialized
                ? (IActionResult) RedirectToAction("Index", UserContext.Permissions.Contains(Permission.Admin)
                    ? "Console"
                    : "UserConsole")
                : View();
        }
    }
}