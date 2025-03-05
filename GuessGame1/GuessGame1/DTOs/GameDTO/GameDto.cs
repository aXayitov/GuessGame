namespace GuessGame1.DTOs.GameDTO
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string SecretNumber { get; set; }
        public int AttemptsLeft { get; set; }
        public bool IsGameOver { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
