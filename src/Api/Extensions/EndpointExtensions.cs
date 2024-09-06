

namespace Api.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder ProduceProblems(this RouteHandlerBuilder builder, params int[] codes)
    {
        return codes
            .Select(code => builder.ProducesProblem(code))
            .Aggregate((builder, result) => builder);
    }
}
