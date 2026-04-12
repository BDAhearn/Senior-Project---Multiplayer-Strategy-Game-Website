using Microsoft.EntityFrameworkCore;
using Multiplayer_Strategy_Game_Website_API.Models;

namespace Multiplayer_Strategy_Game_Website_API.Data
{
    public class GameSiteDbContext : DbContext
    {
        public GameSiteDbContext(DbContextOptions<GameSiteDbContext> options)
            : base(options) { }

        public DbSet<Account> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    playerId = 1,
                    playerName = "techstreet",
                    playerPasswordHash = AccountController.HashPassword("password"),
                    playerDateJoined = new DateTime(2025, 12, 11)
                },
                new Account
                {
                    playerId = 2,
                    playerName = "progamer97",
                    playerPasswordHash = AccountController.HashPassword("123456"),
                    playerDateJoined = new DateTime(2024, 2, 15)
                },
                new Account
                {
                    playerId = 3,
                    playerName = "GameSkayer93",
                    playerPasswordHash = AccountController.HashPassword("qwerty"),
                    playerDateJoined = new DateTime(2026,1,22)
                }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    gameId = 1,
                    gameName = "TicTacToe",
                    gameStatus = "Testing"
                },
                new Game
                {
                    gameId = 2,
                    gameName = "Connect 4",
                    gameStatus = "Testing"
                },
                new Game
                {
                    gameId = 3,
                    gameName = "Connect The Dots",
                    gameStatus = "Closed"
                }
            );

            modelBuilder.Entity<Lobby>().HasData(
                new Lobby
                {
                    lobbyId = 1,
                    lobbyGameId = 1,
                    lobbyHostID = 1,
                    lobbyChallengerId = 2,
                    lobbyVisibility = "public",
                    lobbyStatus = "closed",
                    lobbyChallengerStatus = "won",
                    lobbyHostStatus = "lost",
                    lobbyDateCreated = new DateTime(2026,1,5)
                },
                new Lobby
                {
                    lobbyId = 2,
                    lobbyGameId = 2,
                    lobbyHostID = 2,
                    lobbyChallengerId = 3,
                    lobbyVisibility = "private",
                    lobbyStatus = "closed",
                    lobbyChallengerStatus = "won",
                    lobbyHostStatus = "lost",
                    lobbyDateCreated = new DateTime(2026, 2, 19)
                },
                new Lobby
                {
                    lobbyId = 3,
                    lobbyGameId = 1,
                    lobbyHostID = 1,
                    lobbyVisibility = "public",
                    lobbyStatus = "Open",
                    lobbyDateCreated = new DateTime(2026, 3, 22)
                }
            );
        }
    }
}