namespace DiscordBot.Modules;

public class PingModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "pingを計測します。")]
    public async Task PingCommandAsync()
    {
        var latency = (DateTime.UtcNow - Context.Interaction.CreatedAt).Milliseconds;
        var embedBuilder = new EmbedBuilder()
            .WithTitle("結果 :ping_pong:")
            .WithDescription($"**API Endpoint Ping**: {latency}ms\n" +
                $"**WebSocket Ping**: {Context.Client.Latency}ms")
            .WithFooter($"実行者: {Context.User.GlobalName ?? Context.User.Username}", Context.User.GetDisplayAvatarUrl())
            .WithColor(0x8DCE3E);

        await RespondAsync(embed: embedBuilder.Build());
    }
}
