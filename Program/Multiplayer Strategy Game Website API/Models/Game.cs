namespace Multiplayer_Strategy_Game_Website_API.Models
{
    public class Game
    {
        public int gameId { get; set; }
        public string gameName { get; set; }
        public string gameStatus { get; set; }

        public Game() {}
        
        public Game(int _id, string _name, string _status)
        {
            gameId = _id;
            gameName = _name;
            gameStatus = _status;
        }
    }
}
