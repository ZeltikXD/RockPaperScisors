using RockPaperScisors.Code;

namespace RockPaperScisors
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var game = new GameHandler(args);
            game.StartGame();
        }
    }
}
