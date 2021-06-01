using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDextra.Controllers
{
    public class HousesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
