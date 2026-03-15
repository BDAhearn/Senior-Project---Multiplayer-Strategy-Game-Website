using Microsoft.EntityFrameworkCore;

namespace Multiplayer_Strategy_Game_Website.Models
{
    public class GameSiteContext : DbContext
    {
        public GameSiteContext(DbContextOptions<GameSiteContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

     }
}
