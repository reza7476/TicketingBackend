using Ticketing.Shared.Interfaces;

namespace Ticketing.Infrastructure.Services;

internal class DateTimeAppService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}
