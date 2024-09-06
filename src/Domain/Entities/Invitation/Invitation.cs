﻿using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Invitation;

public sealed class Invitation : Entity
{
    public required InvitationId Id { get; set; }
    public required Status Status { get; set; }
    public required MeetingId MeetingId { get; set; }
    public Meeting.Meeting? Meeting { get; set; }
    public required UserId UserId { get; set; }
    public User.User? User { get; set; }
}