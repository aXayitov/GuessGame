using GuessGame1.Data;
using GuessGame1.Interface;
using GuessGame1.Mappings;
using GuessGame1.Service;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Batteries.Init();

       builder.Services.AddDbContext<GameDbContext>(options =>
            options.UseSqlite("Data Source=game.db"));

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(typeof(Mappings).Assembly);
        builder.Services.AddScoped<IGameService, GameService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}