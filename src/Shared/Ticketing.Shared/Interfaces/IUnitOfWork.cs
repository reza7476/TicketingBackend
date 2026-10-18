using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Shared.Interfaces;

public interface IUnitOfWork:IScope
{
    Task Begin();
    Task Commit();
    Task Complete();
    Task RoleBack();
}
