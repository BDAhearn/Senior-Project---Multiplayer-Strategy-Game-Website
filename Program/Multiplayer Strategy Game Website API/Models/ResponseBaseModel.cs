namespace Multiplayer_Strategy_Game_Website_API.Models
{
    public class ResponseBaseModel
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
