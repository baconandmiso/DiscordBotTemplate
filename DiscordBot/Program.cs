global using Discord;
global using Discord.Interactions;
global using Discord.WebSocket;

global using Microsoft.Extensions.Logging;

using DiscordBot.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

var builder = new HostApplicationBuilder(args);
builder.Configuration.AddEnvironmentVariables("DiscordBot_");

var loggerConfig = new LoggerConfiguration()
    .WriteTo.Console()
#if RELEASE
    .WriteTo.File($".logs.d/log-{DateTime:Now:yy.MM.dd_HH.mm}.log")
#endif
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfig);

builder.Services.AddSingleton(new DiscordSocketClient(
    new DiscordSocketConfig
    {
        GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences | GatewayIntents.MessageContent,
        FormatUsersInBidirectionalUnicode = false,
        UseInteractionSnowflakeDate = false,
        AlwaysDownloadUsers = false,
        LogGatewayIntentWarnings = false,
        LogLevel = LogSeverity.Info
    }
));

builder.Services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>(),
    new InteractionServiceConfig()
    {
        UseCompiledLambda = true,
        LogLevel = LogSeverity.Info
    }
));

builder.Services.AddSingleton<InteractionHandler>();
builder.Services.AddHostedService<DiscordBotService>();

var app = builder.Build();
await app.RunAsync();
