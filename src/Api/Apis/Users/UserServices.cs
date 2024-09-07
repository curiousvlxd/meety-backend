﻿using MediatR;
using Telegram.Bot;
namespace Api.Apis.Users;

public class UserServices(
    ILogger<UserServices> logger,
    ITelegramBotClient client,
    IMediator mediator)
{   
    public ITelegramBotClient client => client;
    public ILogger<UserServices> Logger => logger;
    public IMediator Mediator => mediator;
}
