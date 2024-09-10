namespace RockPaperScisors.Code
{
    public class GameHandler
    {
        readonly string[] _moves;
        readonly GameRuler _ruler;
        readonly GameTable _table;
        string _key = string.Empty;
        string _hmac = string.Empty;

        public GameHandler(string[] args)
        {
            _moves = args;
            _ruler = new(args);
            _table = new(args, _ruler);
        }

        public void StartGame()
        {
            if (!AreValidArgs(out var message))
            {
                WhenInvalidArguments(message!);
                return;
            }

            var compMove = GenerateMove();

        main:
            Console.Clear();
            PrintMenu();
            Console.Write("Enter your move: ");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "?":
                    PrintHelp();
                    goto main;
                case "0":
                    Console.WriteLine("Closing the game.");
                    return;
            }
            if (!int.TryParse(userInput, out var moveIndex))
            {
                Console.WriteLine("Invalid move. Try again.");
                goto main;
            }
            if (!_ruler.IsValidMove(moveIndex - 1))
            {
                Console.WriteLine("Invalid move. Try again.");
                goto main;
            }
            var result = PlayGame(moveIndex - 1, compMove);
            PrintResult(_moves[moveIndex - 1], compMove, result);
            PrintContinueQuestions(out bool wantTo);
            if (wantTo) goto main;
        }

        public void PrintMenu()
        {
            Console.WriteLine("HMAC: {0}", _hmac);
            Console.WriteLine("Available moves:");
            for (int i = 0; i < _moves.Length; i++)
                Console.WriteLine("{0} - {1}", i + 1, _moves[i]);

            Console.WriteLine("0 - Exit");
            Console.WriteLine("? - Help");
        }

        public void PrintResult(string userMove, string machineMove, string result)
        {
            Console.WriteLine("Your move: {0}", userMove);
            Console.WriteLine("Machine move: {0}", machineMove);
            Console.WriteLine("You {0}!", result);
            Console.WriteLine("HMAC Key: {0}", _key);
        }

        public void PrintHelp()
        {
            _table.Print();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private string PlayGame(int userMoveIndex, string compMove)
        {
            var userMove = _moves[userMoveIndex];
            return _ruler.GetGameResult(userMove, compMove);
        }

        private static void PrintContinueQuestions(out bool wantTo)
        {
        tryagain:
            Console.WriteLine("If you want to continue, press 1, otherwise press 0");
            var userInput = Console.ReadLine();
            if(!int.TryParse(userInput, out var result))
            {
                Console.WriteLine("Invalid option. Press any key to continue.");
                Console.ReadKey();
                Console.Write(string.Empty);
                goto tryagain;
            }
            wantTo = Convert.ToBoolean(result);
        }

        private string GenerateMove()
        {
            _key = HmacGenerator.GenerateKey();
            var move = _moves.ElementAtOrDefault(GetRandomMove());
            _hmac = HmacGenerator.GenerateHmac(_key, move ?? string.Empty);
            return move ?? string.Empty;
        }

        private int GetRandomMove()
            => Random.Shared.Next(_moves.Length);

        bool AreValidArgs(out string? message)
        {
            if (!(_moves is not null && _moves.Length >= 3))
            {
                message = "Number of arguments incorrect. There must be 3 or more arguments. \nCorrect: 'dotnet run A B C' or 'app.exe A B C' \nIncorrect: 'dotnet run A B' or 'app.exe A B'";
                return false;
            }
            if (_moves.Length % 2 == 0)
            {
                message = "Number of arguments incorrect. There can only be an odd number of arguments. \nCorrect: 'dotnet run A B C' or 'app.exe A B C' \nIncorrect: 'dotnet run A B C D' or 'app.exe A B C D'";
                return false;
            }
            if (_moves.Length != _moves.Distinct().Count())
            {
                message = "Incorrect arguments. There must be different arguments. \nCorrect: 'dotnet run A B C' or 'app.exe A B C' \nIncorrect: 'dotnet run A A B' or 'app.exe A A B'";
                return false;
            }

            message = null;
            return true;
        }

        static void WhenInvalidArguments(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Restart the program with new arguments. Press any key to close.");
            Console.ReadKey();
        }
    }
}
