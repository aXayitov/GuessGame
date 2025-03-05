using GuessGame1.Entity.Base;

namespace GuessGame1.Entity
{
    public class Game : EntityBase
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public string SecretNumber { get; set; } = "";
        public int AttemptsLeft { get; set; } = 8;
        public bool IsGameOver { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
