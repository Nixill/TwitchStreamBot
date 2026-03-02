using Nixill.Streaming.JoltBot.Data;
using Nixill.Streaming.JoltBot.Twitch.Api;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;

namespace Nixill.Streaming.JoltBot.Twitch.Events.Rewards;

public static class JoltRewardResponse
{
  public static Task CompleteRewardByName(string rewardName, string redemptionUUID)
    => JoltApiClient.WithToken((api, id) => api.Helix.ChannelPoints.UpdateRedemptionStatusAsync(id,
        RewardsJson.RewardKeys[rewardName], [redemptionUUID], new UpdateCustomRewardRedemptionStatusRequest
        {
          Status = TwitchLib.Api.Core.Enums.CustomRewardRedemptionStatus.FULFILLED
        }
      ));

  public static Task CompleteRewardByUUID(string rewardUUID, string redemptionUUID)
    => JoltApiClient.WithToken((api, id) => api.Helix.ChannelPoints.UpdateRedemptionStatusAsync(id,
        rewardUUID, [redemptionUUID], new UpdateCustomRewardRedemptionStatusRequest
        {
          Status = TwitchLib.Api.Core.Enums.CustomRewardRedemptionStatus.FULFILLED
        }
      ));

  public static Task RejectRewardByName(string rewardName, string redemptionUUID)
    => JoltApiClient.WithToken((api, id) => api.Helix.ChannelPoints.UpdateRedemptionStatusAsync(id,
        RewardsJson.RewardKeys[rewardName], [redemptionUUID], new UpdateCustomRewardRedemptionStatusRequest
        {
          Status = TwitchLib.Api.Core.Enums.CustomRewardRedemptionStatus.CANCELED
        }
      ));

  public static Task RejectRewardByUUID(string rewardUUID, string redemptionUUID)
    => JoltApiClient.WithToken((api, id) => api.Helix.ChannelPoints.UpdateRedemptionStatusAsync(id,
        rewardUUID, [redemptionUUID], new UpdateCustomRewardRedemptionStatusRequest
        {
          Status = TwitchLib.Api.Core.Enums.CustomRewardRedemptionStatus.CANCELED
        }
      ));

  public static Task PauseRewardByName(string rewardName)
    => JoltApiClient.WithToken((api, id) => api.Helix.ChannelPoints.UpdateCustomRewardAsync(id,
        RewardsJson.RewardKeys[rewardName], new UpdateCustomRewardRequest
        {
          IsPaused = true
        }
      ));

  public static Task UnpauseRewardByName(string rewardName)
    => JoltApiClient.WithToken((api, id) => api.Helix.ChannelPoints.UpdateCustomRewardAsync(id,
        RewardsJson.RewardKeys[rewardName], new UpdateCustomRewardRequest
        {
          IsPaused = false
        }
      ));
}