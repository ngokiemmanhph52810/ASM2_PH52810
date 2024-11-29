using ASM2_PH52810.Entity;
using ASM2_PH52810.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASM2_PH52810.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        AsmbackendContext _db;

        public HomeController(ILogger<HomeController> logger,AsmbackendContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var get = _db.Players.ToList();
            return new JsonResult(get);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
