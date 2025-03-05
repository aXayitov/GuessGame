namespace GuessGame1.Entity
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PlayerName { get; set; } = "";
        public string SecretNumber { get; set; } = "";
        public int AttemptsLeft { get; set; } = 8;
        public bool IsGameOver { get; set; } = false;
    }
}
