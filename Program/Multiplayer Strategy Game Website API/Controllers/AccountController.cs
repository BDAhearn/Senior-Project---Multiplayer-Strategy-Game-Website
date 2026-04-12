using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Multiplayer_Strategy_Game_Website_API.Data;
using Multiplayer_Strategy_Game_Website_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly GameSiteDbContext _context;
    private readonly IConfiguration _config;

    public AccountController(GameSiteDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }


    [HttpPost("register")]
    public IActionResult CreatePlayer([FromBody] Account newPlayer)
    {
        newPlayer.playerPasswordHash = HashPassword(newPlayer.playerPasswordHash);
        newPlayer.playerDateJoined = DateTime.UtcNow;

        _context.Players.Add(newPlayer);
        _context.SaveChanges();

        return Ok(new
        {
            message = "Account created",
            newPlayer.playerName,
            newPlayer.playerId
        });
    }

    [HttpPost("login")]
    public ActionResult<AccountLoginResponse> Login([FromBody] AccountLoginRequest request)
    {
        var user = _context.Players.FirstOrDefault(u => u.playerName == request.UserName);
        var jwtSettings = _config.GetSection("Jwt");
        var expireTime = double.Parse(jwtSettings["ExpireMinutes"]);

        if (user == null)
        {
            return BadRequest(new AccountLoginResponse
            {
                Success = false,
                Message = "Invalid username",
                Token = "",
                Expire = DateTime.UtcNow
            });
        }

        if (user.playerPasswordHash != HashPassword(request.Password))
        {
            return BadRequest(new AccountLoginResponse
            {
                Success = false,
                Message = "Invalid password",
                Token = "",
                Expire = DateTime.UtcNow 
            });
        }

        var expire = DateTime.UtcNow.AddMinutes(expireTime);
        var token = GenerateJwtToken(user, expire);

        return Ok(new AccountLoginResponse
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            Expire = expire
        });
    }

    [Authorize]
    [HttpGet("secure-test")]
    public IActionResult SecureTest()
    {
        return Ok("You are authenticated!");
    }

    private string GenerateJwtToken(Account user, DateTime expire)
    {
        var jwtSettings = _config.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.playerId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.playerId.ToString()),
            new Claim("Name", user.playerName.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expire,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}