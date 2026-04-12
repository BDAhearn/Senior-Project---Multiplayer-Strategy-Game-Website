namespace Multiplayer_Strategy_Game_Website_API.Models
{
    public class AccountLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Expire { get; set; }
    }
}
