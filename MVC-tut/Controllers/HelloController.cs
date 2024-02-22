using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_tut.Controllers
{
    public class HelloController : Controller
    {
      //Get: /Hello/
        public IActionResult Index()
        {
            return View();
        }
        //Get: /Hello/Welcome

        public string Welcome()
        {
            return "welcome dude";
        }
    }
}
