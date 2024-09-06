using Microsoft.AspNetCore.Http;
namespace Infrastructure.Logging;

public class RequestBodyLoggingMiddleware(RequestDelegate next, ILogger logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();

            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);

            buffer.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(buffer).ReadToEndAsync();
            if (!string.IsNullOrEmpty(bodyText))
            {
                logger.Information($"Request Body: {bodyText}");
            }

            buffer.Seek(0, SeekOrigin.Begin);
            context.Request.Body.Position = 0;
        }

        await next(context);
    }
}
