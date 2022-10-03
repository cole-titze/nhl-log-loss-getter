using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LogLoss;

var logLossGetter = new LogLossGetter();

// Build service collection
var collection = new ServiceCollection();
collection.AddLogging(b => {
    b.SetMinimumLevel(LogLevel.Information);
});
var sp = collection.BuildServiceProvider();

// Get logger and run main
using (var scope = sp.CreateScope())
{
    string? gamesConnectionString = Environment.GetEnvironmentVariable("GAMES_DATABASE");

    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    if (gamesConnectionString == null)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.Local.json").Build();
        gamesConnectionString = config.GetConnectionString("GAMES_DATABASE");
    }
    if (gamesConnectionString == null)
        throw new Exception("Connection String Null");

    await logLossGetter.Main(logger, gamesConnectionString);
}