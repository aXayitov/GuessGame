using GuessGame1.Data;
using GuessGame1.Entity;
using GuessGame1.Interface;

namespace GuessGame1.Service
{
    public class GameService(GameDbContext context) : IGameService
    {
        private readonly GameDbContext _context = context
            ?? throw new ArgumentNullException(nameof(context));

        public Game StartGame(string playerName)
        {
            var newGame = new Game
            {
                PlayerName = playerName,
                SecretNumber = SecretNumber()
            };
            _context.Games.Add(newGame);
            _context.SaveChanges();
            return newGame;
        }

        public string MakeGuess(Guid gameId, string guess)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == gameId);

            if (game == null || game.IsGameOver) return "Game is over or you lost.";

            --game.AttemptsLeft;

            if (guess == game.SecretNumber)
            {
                game.IsGameOver = true;
                _context.SaveChanges();
                return "Win!";
            }

            if (game.AttemptsLeft == 0)
            {
                game.IsGameOver = true;
                _context.SaveChanges();
                return $"Lose! Secret number: {game.SecretNumber}";
            }

            _context.SaveChanges(); 

            Console.WriteLine($"After Guess - Game ID: {game.Id}, Attempts Left: {game.AttemptsLeft}");

            return Result(game.SecretNumber, guess);
        }

        private string Result(string secret, string guess)
        {
            int m = 0;
            int p = 0;
            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == secret[i]) p++;
                else if (secret.Contains(guess[i])) m++;
            }
            return $"M:{m}; P:{p}";
        }

        private string SecretNumber()
        {
            Random rnd = new Random();
            return string.Concat(Enumerable.Range(0, 10).OrderBy(_ => rnd.Next()).Take(4));
        }
    }
}
