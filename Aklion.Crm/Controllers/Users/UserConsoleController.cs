﻿using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Users
{
    [Route("Console")]
    public class UserConsoleController : BaseController
    {
        public IActionResult Index()
        {
            return View("~/Views/User/Console/Index.cshtml");
        }
    }
}