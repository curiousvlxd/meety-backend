﻿namespace Telegram.Bot.Requests;

/// <summary>Use this method to reopen a closed topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights, unless it is the creator of the topic.<para>Returns: </para></summary>
public partial class ReopenForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier for the target message thread of the forum topic</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageThreadId { get; set; }

    /// <summary>Instantiates a new <see cref="ReopenForumTopicRequest"/></summary>
    public ReopenForumTopicRequest() : base("reopenForumTopic") { }
}
