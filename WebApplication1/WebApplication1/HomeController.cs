using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication1
{
    public class HomeController : Controller 
    {
        ILog _log;

        public HomeController(ILog log)
        {
            _log = log;
        }

        public IActionResult Index()
        {
            var services = this.HttpContext.RequestServices;
            var log = (ILog)services.GetService(typeof(ILog));

            _log.info("Executing /home/index.");

            return View();
        }

        public IActionResult Index([FromServices] ILog log)
        {
            log.info("index method executing.");

            return View();
        }
    }
}
