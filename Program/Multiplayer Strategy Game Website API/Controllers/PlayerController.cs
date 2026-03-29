using Microsoft.AspNetCore.Mvc;
using Multiplayer_Strategy_Game_Website_API.Data;
using Multiplayer_Strategy_Game_Website_API.Models;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/players")]
public class PlayerController : ControllerBase
{
    private readonly GameSiteDbContext _context;
    private string username;

    public PlayerController(GameSiteDbContext context)
    {
        _context = context;
    }
        
    [HttpGet(Name = "GetPlayers")]
    public IActionResult Get()
    {
        var players = _context.Players
            .Select(p => new
            {
                p.playerName,
                p.playerDateJoined
            })
            .ToList();

        return Ok(players);
    }

    [HttpPost]
    public IActionResult CreatePlayer([FromBody] Player newPlayer)
    {
        newPlayer.playerPasswordHash = HashPassword(newPlayer.playerPasswordHash);

        _context.Players.Add(newPlayer);
        _context.SaveChanges();

        return Ok(newPlayer);
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _context.Players.FirstOrDefault(u => u.playerName == request.Username);

        if (user == null)
            return BadRequest("Username is required");

        if (user.playerPasswordHash != HashPassword(request.Password))
            return BadRequest("Username or Password is incorrect");

        return Ok(new
        {
            message = "Login successful",
            user.playerName,
            user.playerId
        });
    }

    public static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}