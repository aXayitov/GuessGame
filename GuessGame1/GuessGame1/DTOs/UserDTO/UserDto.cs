using GuessGame1.DTOs.GameDTO;

namespace GuessGame1.DTOs.UserDTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public List<GameDto> Games { get; set; }
    }
}
