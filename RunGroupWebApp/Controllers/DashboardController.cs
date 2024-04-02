using Microsoft.AspNetCore.Mvc;

namespace RunGroupWebApp.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
