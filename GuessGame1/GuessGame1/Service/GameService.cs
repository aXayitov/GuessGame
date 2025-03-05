using AutoMapper;
using GuessGame1.Data;
using GuessGame1.DTOs.UserDTO;
using GuessGame1.Entity;
using GuessGame1.Interface;
using Microsoft.EntityFrameworkCore;

namespace GuessGame1.Service
{
    public class GameService(IMapper mapper, GameDbContext context) : IGameService
    {
        private readonly GameDbContext _context = context
            ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));

        public Game StartGame(string playerName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == playerName);
            if (user == null)
            {
                user = new User { UserName = playerName };
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            int todayGames = _context.Games.Count(g => g.UserId == user.Id && g.CreatedAt.Date == DateTime.UtcNow.Date);
            if (todayGames >= 8)
                throw new Exception("You have reached the daily limit of 8 games!");

            var newGame = new Game
            {
                UserId = user.Id,
                SecretNumber = SecretNumber()
            };

            _context.Games.Add(newGame);
            _context.SaveChanges();

            return newGame;
        }

        private string SecretNumber()
        {
            Random rand = new Random();
            return rand.Next(1000, 9999).ToString();
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

        public List<UserDto> GetUsers(string UserName)
        {
            var users = _context.Users
            .Include(u => u.Games)
            .Where(u => string.IsNullOrWhiteSpace(UserName) || u.UserName.Contains(UserName))
            .ToList();

            return _mapper.Map<List<UserDto>>(users);
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
    }
}
