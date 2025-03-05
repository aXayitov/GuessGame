using GuessGame1.Entity;

namespace GuessGame1.Interface
{
    public interface IGameService
    {
        Game StartGame(string PlayerName);
        string MakeGuess(Guid gameId, string guess);
    }
}
