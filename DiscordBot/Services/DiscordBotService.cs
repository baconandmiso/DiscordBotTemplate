using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DiscordBot.Services;

public class DiscordBotService(DiscordSocketClient client, InteractionService interactions, InteractionHandler interactionHandler,
    IConfiguration configuration, ILogger<DiscordBotService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        client.Log += Log;
        interactions.Log += Log;

        client.Ready += Ready;

        await interactionHandler.InitializeAsync();
        await client.LoginAsync(TokenType.Bot, configuration["DiscordBot:Token"]);
        await client.StartAsync();

        await Task.Delay(-1, cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        if (ExecuteTask == null)
            return Task.CompletedTask;

        base.StopAsync(cancellationToken);
        return client.StopAsync();
    }

    public Task Log(LogMessage message)
    {
        var severity = message.Severity switch
        {
            LogSeverity.Critical => LogLevel.Critical,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Info => LogLevel.Information,
            LogSeverity.Verbose => LogLevel.Trace,
            LogSeverity.Debug => LogLevel.Debug,
            _ => LogLevel.Information
        };

        logger.Log(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        return Task.CompletedTask;
    }

    public Task Ready()
    {
        _ = Task.Run(async () =>
        {
            try
            {
                // ApplicationCommands の登録をここで行う。
                await interactions.RegisterCommandsToGuildAsync(ulong.Parse(configuration["DiscordBot:GuildId"] ?? ""));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Message}", ex.Message);
            }
        });

        return Task.CompletedTask;
    }
}
