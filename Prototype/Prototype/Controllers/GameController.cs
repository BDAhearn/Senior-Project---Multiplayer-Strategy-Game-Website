using Microsoft.AspNetCore.Mvc;

namespace Multiplayer_Strategy_Game_Website.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
