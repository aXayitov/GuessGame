using GuessGame1.DTOs.UserDTO;
using GuessGame1.Entity;

namespace GuessGame1.Interface
{
    public interface IGameService
    {
        Game StartGame(string PlayerName);
        string MakeGuess(Guid gameId, string guess);
        List<UserDto> GetUsers(string UseName);
    }
}
