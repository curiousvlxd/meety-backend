using UseCases.Abstractions.Messaging;
namespace UseCases.Features.Meetings.Create;

public record CreateMeetingCommand(string Name, string UserId, string Agenda, DateTime Date) : ICommand;
