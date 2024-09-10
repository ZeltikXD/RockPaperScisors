namespace RockPaperScisors.Code.Enums
{
    /// <summary>
    /// The game result. The game result issued is for a user result.
    /// </summary>
    public class GameResult
    {
        private readonly string _resultStr;
        private readonly int _resultCode;
        private static readonly GameResult _win = new("Win", 1);
        private static readonly GameResult _lose = new("Lose", -1);
        private static readonly GameResult _draw = new("Draw", 0);

        private GameResult(string resultStr, int resultCode)
        {
            _resultStr = resultStr;
            _resultCode = resultCode;
        }

        public static GameResult Win => _win;
        public static GameResult Lose => _lose;
        public static GameResult Draw => _draw;

        public static implicit operator string(GameResult result)
        {
            return result.ToString();
        }

        public static implicit operator int(GameResult result)
        {
            return result._resultCode;
        }

        public override string ToString()
            => _resultStr;
    }
}
