using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
