using Microsoft.Extensions.Logging;
using NReco.Logging.File;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace Nixill.Streaming.JoltBot.Twitch;

public static class TwitchBot
{
  internal static TwitchClient Client;

  static ILogger Logger = Log.Factory.CreateLogger("TwitchBot");

  public static async Task SetUp(AuthInfo auth, string channelName)
  {
    ConnectionCredentials credentials = new ConnectionCredentials(auth.Name, auth.Token);

    Client = new TwitchClient(loggerFactory: Log.Factory);

    Client.OnConnected += ConnectHandler;
    Client.OnJoinedChannel += JoinHandler;
    Client.OnFailureToReceiveJoinConfirmation += FailJoinHandler;
    Client.OnSendReceiveData += DataHandler;

    Logger.LogInformation("Attempting to connect...");
    Client.Initialize(credentials, channelName);
    await Client.ConnectAsync();
  }

  public static Task ConnectHandler(object sender, OnConnectedEventArgs ev)
  {
    Logger.LogInformation("Connection established.");
    return Task.CompletedTask;
  }

  public static async Task JoinHandler(object sender, OnJoinedChannelArgs ev)
  {
    Logger.LogInformation($"Jolt chatbot connected to {ev.Channel}.");
    await Client.SendMessageAsync(ev.Channel, "Jolt chatbot connected.");
  }

  public static Task FailJoinHandler(object sender, OnFailureToReceiveJoinConfirmationArgs ev)
  {
    Logger.LogError($"Failed to join {ev.Exception.Channel}: {ev.Exception.Details ?? "No further details."}");
    return Task.CompletedTask;
  }

  public static Task DataHandler(object sender, OnSendReceiveDataArgs ev)
  {
    Logger.LogTrace($"{ev.Direction} {ev.Data}");
    return Task.CompletedTask;
  }
}