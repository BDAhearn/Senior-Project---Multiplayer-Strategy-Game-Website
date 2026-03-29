using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multiplayer_Strategy_Game_Website_API.Data;

namespace Multiplayer_Strategy_Game_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly GameSiteDbContext _context;
        public LobbyController(GameSiteDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetLobbies")]
        public IActionResult Get()
        {
            var lobbies = _context.Lobbies
                .Select(l => new
                {
                    l.lobbyId,

                    gameName = _context.Games
                        .Where(g => g.gameId == l.lobbyGameId)
                        .Select(g => g.gameName)
                        .FirstOrDefault(),

                    hostName = _context.Players
                        .Where(p => p.playerId == l.lobbyHostID)
                        .Select(p => p.playerName)
                        .FirstOrDefault(),

                    challengerName = _context.Players
                        .Where(p => p.playerId == l.lobbyChallengerId)
                        .Select(p => p.playerName)
                        .FirstOrDefault(),

                    l.lobbyStatus,
                    l.lobbyDateCreated,
                    l.lobbyVisibility
                })
                .ToList();

            return Ok(lobbies);
        }

    }
}
