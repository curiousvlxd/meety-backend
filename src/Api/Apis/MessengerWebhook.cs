using MediatR;
using UseCases.Features.Messengers.Telegram.Webhook;
namespace Api.Apis;

public static class MessengerWebhookEndpoint
{
    public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/messenger/webhook", async (Mediator mediator) =>
            {
                await mediator.Send(new HandleWebhookCommand());
                Console.WriteLine("[POST] Sending weather forecast");
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}
