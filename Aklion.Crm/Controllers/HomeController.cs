using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class HomeController : Controller
    {
      //  [Route("/{route?}")]
        public IActionResult Index(string route)
        {
            switch (route.ToLower().Trim())
            {
                case "account":
                    ViewBag.Title = "Аккаунт";
                    return View();

                case "users":
                    ViewBag.Title = "Пользователи";
                    return View();

                case "stores":
                    ViewBag.Title = "Магазины";
                    return View();

                case "error":

                    return Error();
            }

            return View();
        }

      //  [Route("/account/get")]
        public JsonResult AccountData()
        {
            var data = new
            {
                Success = true,
                ErrorMessage = string.Empty,
                Data = new
                {
                    Login = "auk073@mail.ru"
                }
            };

            return Json(data);
        }

        public JsonResult AccessDenied()
        {
            var data = new
            {
                Success = false,
                ErrorMessage = "У Вас недостаточно прав",
                Data = (object) null
            };

            return Json(data);
        }

        public JsonResult Error()
        {
            var data = new
            {
                Success = false,
                ErrorMessage = "Произошла ошибка",
                Data = (object) null
            };

            return Json(data);
        }

        public IActionResult Test()
        {
            return View();
        }

        public JsonResult GetData()
        {
            var data = new List<object>
            {
                new
                {
                    Id = 5,
                    Name = "ываыва",
                    Author = "asdasda",
                    Year = 2001,
                    Price = 123
                }
            };

            return Json(data);
        }
    }
}