using System.ComponentModel.DataAnnotations;

namespace Multiplayer_Strategy_Game_Website.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

    }
}
