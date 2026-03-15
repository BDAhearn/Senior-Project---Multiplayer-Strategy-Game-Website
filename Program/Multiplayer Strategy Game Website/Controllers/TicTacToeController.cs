using Microsoft.AspNetCore.Mvc;
using Multiplayer_Strategy_Game_Website.Models;

namespace Multiplayer_Strategy_Game_Website.Controllers
{
    public class TicTacToeController : Controller
    {
        private static TicTacToe _game = new TicTacToe();

        public IActionResult TicTacToe()
        {
            return View("~/Views/Game/TicTacToe.cshtml", _game);
        }

        [HttpPost]
        public IActionResult Select(string button)
        {
            var cell = _game.GetType().GetProperty(button);

            if (cell != null && cell.CanWrite && string.IsNullOrEmpty((string)cell.GetValue(_game)))
            {
                cell.SetValue(_game, _game.Turn);
                _game.Turn = NextTurn(_game.Turn);
                _game.Winner = CheckWinner(_game);
            }

            return View("~/Views/Game/TicTacToe.cshtml", _game);
        }

        public IActionResult NewGame()
        {
            _game = new TicTacToe();
            return View("~/Views/Game/TicTacToe.cshtml", _game);
        }

        // Swap turns
        private static string NextTurn(string turn)
        {
            return turn == "X" ? "O" : "X";
        }

        static string CheckWinner(TicTacToe model)
        {
            var lines = new[]
            {
                new[] { model.A1, model.A2, model.A3 },
                new[] { model.B1, model.B2, model.B3 },
                new[] { model.C1, model.C2, model.C3 },
                new[] { model.A1, model.B1, model.C1 },
                new[] { model.A2, model.B2, model.C2 },
                new[] { model.A3, model.B3, model.C3 },
                new[] { model.A1, model.B2, model.C3 },
                new[] { model.A3, model.B2, model.C1 }
            };

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line[0]) && line.All(c => c == line[0]))
                {
                    return line[0];
                }
            }

            if (CheckDraw(model))
            {
                return "Draw";
            }

            return string.Empty;
        }

        static bool CheckDraw(TicTacToe model)
        {
            return !string.IsNullOrEmpty(model.A1) &&
                   !string.IsNullOrEmpty(model.A2) &&
                   !string.IsNullOrEmpty(model.A3) &&
                   !string.IsNullOrEmpty(model.B1) &&
                   !string.IsNullOrEmpty(model.B2) &&
                   !string.IsNullOrEmpty(model.B3) &&
                   !string.IsNullOrEmpty(model.C1) &&
                   !string.IsNullOrEmpty(model.C2) &&
                   !string.IsNullOrEmpty(model.C3);
        }
    }
}