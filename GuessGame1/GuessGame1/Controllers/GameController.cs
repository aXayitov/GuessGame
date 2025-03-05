using GuessGame1.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GuessGame1.Controllers;

public class GameController(IGameService gameService) : Controller
{
    private readonly IGameService _gameService = gameService 
        ?? throw new ArgumentNullException(nameof(gameService));

    [HttpPost("start")]
    public IActionResult StartGame([FromQuery] string playerName)
    {
        var game = _gameService.StartGame(playerName);
        return Ok(new { gameId = game.Id, message = "Goooo!" });
    }

    [HttpPost("guess")]
    public IActionResult MakeGuess([FromQuery] Guid gameId, [FromQuery] string guess)
    {
        if (guess.Length != 4 || !guess.All(char.IsDigit))
            return BadRequest("Gues must be consists 4 number!");

        var result = _gameService.MakeGuess(gameId, guess);
        return Ok(new { message = result });
    }
}
