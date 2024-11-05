using Microsoft.AspNetCore.Mvc;

namespace SignalEProject.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
