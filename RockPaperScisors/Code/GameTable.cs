using ConsoleTables;

namespace RockPaperScisors.Code
{
    public class GameTable(string[] userArgs, GameRuler gameRuler)
    {
        readonly string[] _defaultMoves = userArgs;
        readonly GameRuler _ruler = gameRuler;

        public void Print()
        {
            var table = new ConsoleTable(["v PC \\ User >", .. _defaultMoves]);
            foreach (var move in _defaultMoves)
                table.Rows.Add(GenerateRow(move));

            table.Write(Format.Alternative);
        }

        private string[] GenerateRow(string userMove)
        {
            var row = new List<string>() { userMove };
            var cells = _defaultMoves.Select(x => GenerateCell(userMove, x));
            row.AddRange(cells);
            return [.. row];
        }

        private string GenerateCell(string userMove, string compMove)
            => _ruler.GetGameResult(userMove, compMove);
        
    }
}
