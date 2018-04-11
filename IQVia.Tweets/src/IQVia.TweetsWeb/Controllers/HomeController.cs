using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IQVia.TweetsWeb.Models;

namespace IQVia.TweetsWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var model = new List<Tweet> { new Tweet { Id = "1", Stamp = "2018/04/01", Text = "HI" }};

            //return View(model);
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
