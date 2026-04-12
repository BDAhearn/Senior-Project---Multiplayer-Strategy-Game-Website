using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multiplayer_Strategy_Game_Website_API.Data;

namespace Multiplayer_Strategy_Game_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameSiteDbContext _context;
        public GameController(GameSiteDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetGames")]
        public IActionResult Get()
        {
            var games = _context.Games
                .Select(g => new
                {
                    g.gameName,
                    g.gameStatus
                })
                .ToList();

            return Ok(games);
        }

        [HttpGet("available")]
        public IActionResult GetAvailableGames()
        {
            var games = _context.Games
                .Where(g => g.gameStatus == "Open" || g.gameStatus == "Testing")
                .Select(g => new
                {
                    g.gameId,
                    g.gameName,
                    g.gameStatus
                })
                .ToList();

            return Ok(games);
        }
    }
}
