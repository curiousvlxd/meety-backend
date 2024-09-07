﻿namespace Telegram.Bot.Requests;

/// <summary>Use this method to get up-to-date information about the chat.<para>Returns: A <see cref="ChatFullInfo"/> object on success.</para></summary>
public partial class GetChatRequest : RequestBase<ChatFullInfo>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Instantiates a new <see cref="GetChatRequest"/></summary>
    public GetChatRequest() : base("getChat") { }
}
