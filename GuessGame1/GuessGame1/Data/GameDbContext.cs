using GuessGame1.Entity;
using Microsoft.EntityFrameworkCore;

namespace GuessGame1.Data;

public class GameDbContext : DbContext
{
    public virtual DbSet<Game> Games {  get; set; } 
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {

    }
}
