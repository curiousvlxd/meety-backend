using System.Text.Json;
namespace Infrastructure.Logging;

public class HttpLoggingHandler(ILogger logger) : DelegatingHandler
{   
    private static readonly JsonSerializerOptions SerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Guid id = Guid.NewGuid();
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            logger.Information("[{Id}] Request: {Request}", id, request);
            
            if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
            {
               var json = JsonSerializer.Serialize(request.Content, SerializerOptions);
               logger.Information("[{Id}] Request Content: {RequestContent}", id, json);
            }
            
            logger.Information("[{Id}] Response: {Response}", id, response);
            return response;
        }
}
