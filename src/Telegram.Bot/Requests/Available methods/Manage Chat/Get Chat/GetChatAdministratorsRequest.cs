﻿namespace Telegram.Bot.Requests;

/// <summary>Use this method to get a list of administrators in a chat, which aren't bots.<para>Returns: An Array of <see cref="ChatMember"/> objects.</para></summary>
public partial class GetChatAdministratorsRequest : RequestBase<ChatMember[]>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Instantiates a new <see cref="GetChatAdministratorsRequest"/></summary>
    public GetChatAdministratorsRequest() : base("getChatAdministrators") { }
}
