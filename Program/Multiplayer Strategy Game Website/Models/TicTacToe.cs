namespace Multiplayer_Strategy_Game_Website.Models
{
    public class TicTacToe
    {
        //Holds Values for cells
        public string A1 { get; set; } = string.Empty;
        public string A2 { get; set; } = string.Empty;
        public string A3 { get; set; } = string.Empty;
        public string B1 { get; set; } = string.Empty;
        public string B2 { get; set; } = string.Empty;
        public string B3 { get; set; } = string.Empty;
        public string C1 { get; set; } = string.Empty;
        public string C2 { get; set; } = string.Empty;
        public string C3 { get; set; } = string.Empty;

        //holds value for winner
        public string Winner { get; set; } = string.Empty;

        //holds value for who's turn it is
        public string Turn { get; set; } = "X";
    }
}
