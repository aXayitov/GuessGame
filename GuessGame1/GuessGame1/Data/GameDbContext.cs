using GuessGame1.Entity;
using Microsoft.EntityFrameworkCore;

namespace GuessGame1.Data;

public class GameDbContext : DbContext
{
    public virtual DbSet<Game> Games {  get; set; } 
    public virtual DbSet<User> Users { get; set; }
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Game

        modelBuilder.Entity<Game>().HasKey(e => e.Id);

        modelBuilder.Entity<Game>().
            HasOne(g => g.User)
            .WithMany(u => u.Games)
            .HasForeignKey(g => g.UserId);

        #endregion

        #region User

        modelBuilder.Entity<User>().HasKey(e => e.Id);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Games)
            .WithOne(g => g.User)
            .HasForeignKey(g => g.UserId);

        #endregion
    }
}
