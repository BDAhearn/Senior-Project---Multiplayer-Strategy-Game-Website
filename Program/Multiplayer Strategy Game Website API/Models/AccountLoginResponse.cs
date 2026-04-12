namespace Multiplayer_Strategy_Game_Website_API.Models
{
    public class AccountLoginResponse : ResponseBaseModel
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
