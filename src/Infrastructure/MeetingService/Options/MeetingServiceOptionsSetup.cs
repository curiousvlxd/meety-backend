using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Infrastructure.MeetingService.Options;

public class MeetingServiceOptionsSetup(IConfiguration configuration) : IConfigureOptions<MeetingServiceOptions>
{
    private const string SectionName = "MeetingService";

    public void Configure(MeetingServiceOptions serviceOptions) => configuration.GetSection(SectionName).Bind(serviceOptions);
}
