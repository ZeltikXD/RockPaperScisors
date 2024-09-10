using RockPaperScisors.Code.Enums;

namespace RockPaperScisors.Code
{
    public class GameRuler(string[] moves)
    {
        private readonly string[] _defaultMoves = moves;

        public bool IsValidMove(int moveIndex)
        {
            return _defaultMoves.ElementAtOrDefault(moveIndex) is not null;
        }

        public GameResult GetGameResult(string userMove, string machineMove)
        {
            var userIndex = Array.IndexOf(_defaultMoves, userMove);
            var machineIndex = Array.IndexOf(_defaultMoves, machineMove);

            if (userIndex == machineIndex)
                return GameResult.Draw;

            // Calculate distance in circular direction(in both directions)
            int forwardDistance = (machineIndex - userIndex + _defaultMoves.Length) % _defaultMoves.Length;

            // Half of the next movements is the victory condition
            if (forwardDistance <= _defaultMoves.Length / 2)
                return GameResult.Win;

            return GameResult.Lose;
        }
    }
}
