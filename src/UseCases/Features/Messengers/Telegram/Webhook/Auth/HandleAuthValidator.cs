﻿using FluentValidation;
namespace UseCases.Features.Messengers.Telegram.Webhook.Auth;

public class HandleAuthValidator : AbstractValidator<HandleAuthCommand>
{
    public HandleAuthValidator()
    {
        RuleFor(x => x.MessengerUserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("MessengerUserId is required.");
    }
}
