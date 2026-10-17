using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Shared.Interfaces;

public interface IDateTimeService : IService
{

    DateTime Now { get; }
}
