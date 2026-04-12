using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Multiplayer_Strategy_Game_Website_API.Models
{
    public class Account
    {
        [Key]
        public int playerId { get; set; }
        public string playerName { get; set; }
        public string playerPasswordHash { get; set; }
        public DateTime playerDateJoined { get; set; }

        public Account() { }

        public Account (int _ID, string _playerName, string _playerPasswordHash, DateTime _playerDateJoined)
        {
            playerId = _ID;
            playerName = _playerName;
            playerPasswordHash = _playerPasswordHash;
            playerDateJoined = _playerDateJoined;
        }
    }
}
