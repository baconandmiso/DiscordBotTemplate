namespace DiscordBot.Modules.Ping;

public class PingModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "pingを計測します。")]
    public async Task PingCommandAsync()
    {
        var embedBuilder = new EmbedBuilder()
            .WithTitle("結果 :ping_pong:")
            .WithDescription($"WebSocket Ping: {Context.Client.Latency}ms\n")
            .WithFooter($"実行者: {Context.User.GlobalName ?? Context.User.Username}", Context.User.GetAvatarUrl());

        await RespondAsync(embed: embedBuilder.Build(), ephemeral: true);
        var response = await GetOriginalResponseAsync();

        var latency = (DateTime.Now - response.CreatedAt).Milliseconds;
        embedBuilder.Description += $"API Endpoint Ping: {latency}ms";

        await ModifyOriginalResponseAsync(x => x.Embed = embedBuilder.Build());
    }
}
